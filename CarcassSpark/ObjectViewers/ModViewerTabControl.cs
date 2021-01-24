﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectTypes;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using CarcassSpark.DictionaryViewers;
using CarcassSpark.Flowchart;
using CarcassSpark.Tools;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections;
using MindFusion.Json;
using System.Windows.Forms.VisualStyles;

namespace CarcassSpark.ObjectViewers
{
    public partial class ModViewerTabControl : UserControl
    {
        public bool isVanilla, editMode, valid = false;

        public ContentSource Content = new ContentSource();
        
        private string mostRecentAspectsGroup = "aspects";
        private string mostRecentElementsGroup = "elements";
        private string mostRecentRecipesGroup = "recipes";
        private string mostRecentDecksGroup = "decks";
        private string mostRecentLegaciesGroup = "legacies";
        private string mostRecentEndingsGroup = "endings";
        private string mostRecentVerbsGroup = "verbs";
        
        public ModViewerTabControl(string location, bool isVanilla, bool newMod)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Content.currentDirectory = location;
            saveFileDialog.InitialDirectory = location;
            openFileDialog.InitialDirectory = location;
            this.isVanilla = isVanilla;
            if (!isVanilla && newMod)
            {
                // if the user cancels the synopsis creation for a new mod, invalidate the control and abort loading.
                if (!CreateSynopsis())
                {
                    valid = false;
                    return;
                }
            }
            // if loading content is successful
            if (LoadContent())
            {
                SetEditingMode(Content.GetCustomManifestBool("EditMode") ?? !isVanilla);
                Utilities.ContentSources.Add(isVanilla ? "Vanilla" : Content.synopsis.name, Content);
                valid = true;
            }
            else
            {
                valid = false;
                // MessageBox.Show("Failed to load content source.");
            }
        }

        public void SetEditingMode(bool editing)
        {
            editMode = editing;
            deleteSelectedAspectToolStripMenuItem.Visible = editing;
            deleteSelectedElementToolStripMenuItem.Visible = editing;
            deleteSelectedRecipeToolStripMenuItem.Visible = editing;
            deleteSelectedDeckToolStripMenuItem.Visible = editing;
            deleteSelectedLegacyToolStripMenuItem.Visible = editing;
            deleteSelectedEndingToolStripMenuItem.Visible = editing;
            deleteSelectedVerbToolStripMenuItem.Visible = editing;
            duplicateSelectedAspectToolStripMenuItem.Visible = editing;
            duplicateSelectedElementToolStripMenuItem.Visible = editing;
            duplicateSelectedRecipeToolStripMenuItem.Visible = editing;
            duplicateSelectedDeckToolStripMenuItem.Visible = editing;
            duplicateSelectedLegacyToolStripMenuItem.Visible = editing;
            duplicateSelectedEndingToolStripMenuItem.Visible = editing;
            duplicateSelectedVerbToolStripMenuItem.Visible = editing;
            newAspectToolStripMenuItem.Visible = editing;
            newElementToolStripMenuItem.Visible = editing;
            newRecipeToolStripMenuItem.Visible = editing;
            newDeckToolStripMenuItem.Visible = editing;
            newLegacyToolStripMenuItem.Visible = editing;
            newEndingToolStripMenuItem.Visible = editing;
            newVerbToolStripMenuItem.Visible = editing;
            setGroupAspectToolStripMenuItem.Visible = editing;
            setGroupDeckToolStripMenuItem.Visible = editing;
            setGroupElementToolStripMenuItem.Visible = editing;
            setGroupEndingToolStripMenuItem.Visible = editing;
            setGroupLegacyToolStripMenuItem.Visible = editing;
            setGroupRecipeToolStripMenuItem.Visible = editing;
            setGroupVerbToolStripMenuItem.Visible = editing;
        }

        public bool LoadContent()
        {
            try
            {
                aspectsListView.Items.Clear();
                Content.Aspects.Clear();
                elementsListView.Items.Clear();
                Content.Elements.Clear();
                recipesListView.Items.Clear();
                Content.Recipes.Clear();
                decksListView.Items.Clear();
                Content.Decks.Clear();
                legaciesListView.Items.Clear();
                Content.Legacies.Clear();
                endingsListView.Items.Clear();
                Content.Endings.Clear();
                verbsListView.Items.Clear();
                Content.Verbs.Clear();
                if (!isVanilla)
                {
                    // If there is no synopsis, try to create one. If no synopsis ends up loaded or created, return false so the tab can be canceled
                    if (!CheckForSynopsis())
                    {
                        return false;
                    }
                    if (Directory.Exists(Content.currentDirectory + "\\content\\"))
                    {
                        foreach (string file in Directory.EnumerateFiles(Content.currentDirectory + "\\content\\", "*.json", SearchOption.AllDirectories))
                        {
                            using (FileStream fs = new FileStream(file, FileMode.Open))
                            {
                                LoadFile(fs, file);
                            }
                        }
                        // mod loaded successfully
                        return true;
                    }
                    return false;
                }
                else
                {
                    Content.synopsis = new Synopsis("Vanilla", "Weather Factory", null, "Content from Cultist Simulator", null);
                    foreach (string file in Directory.EnumerateFiles(Content.currentDirectory, "*.json", SearchOption.AllDirectories))
                    {
                        using (FileStream fs = new FileStream(file, FileMode.Open))
                        {
                            LoadFile(fs, file);
                        }
                    }
                    return true;
                }
            }
            // mod failed to load catastrophically
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Content Source Loading Failed");
                return false;
            }
        }

        public bool CreateSynopsis()
        {
            SynopsisViewer mv = new SynopsisViewer(new Synopsis());
            if (mv.ShowDialog() == DialogResult.OK)
            {
                Content.synopsis = mv.displayedSynopsis;
                SaveMod();
                return true;
            }
            return false;
        }

        public bool CheckForSynopsis()
        {
            if (File.Exists(Content.currentDirectory + "/CarcassSpark.Manifest.json"))
            {
                using (FileStream fs = new FileStream(Content.currentDirectory + "/CarcassSpark.Manifest.json", FileMode.Open))
                {
                    LoadCustomManifest(fs);
                }
            }
            string manifestPath = Content.currentDirectory + "/manifest.json";
            string synopsisPath = Content.currentDirectory + "/synopsis.json";
            // if manifest.json still exists, load it, save it as synopsis.json, then delete manifest.json
            if (File.Exists(manifestPath))
            {
                using (FileStream fs = new FileStream(manifestPath, FileMode.Open))
                {
                    LoadSynopsis(fs);
                }
                SaveManifests(Content.currentDirectory);
                File.Delete(manifestPath);
                return true;
            }
            // otherwise if we have synopsis.json, load that
            else if (File.Exists(synopsisPath))
            {
                using (FileStream fs = new FileStream(synopsisPath, FileMode.Open))
                {
                    LoadSynopsis(fs);
                }
                return true;
            }
            else
            {
                // no manifest so we'll try to make one
                if (MessageBox.Show("synopsis.json not found in selected directory, are you creating a new mod?", "No Manifest", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // return true if everything's going good
                    if (CreateSynopsis()) return true;
                    // otherwise return false so I can abort the creation of the tab
                    else return false;
                }
                return false;
            }
        }

        public void LoadSynopsis(FileStream file)
        {
            // string fileText = new StreamReader(file).ReadToEnd();
            // Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            Content.synopsis = JsonConvert.DeserializeObject<Synopsis>(new StreamReader(file).ReadToEnd());
            Text = Content.synopsis.name;
        }

        public void LoadCustomManifest(FileStream file)
        {
            // string fileText = new StreamReader(file).ReadToEnd();
            // Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            Content.CustomManifest = JsonConvert.DeserializeObject<JObject>(new StreamReader(file).ReadToEnd());
        }

        public void LoadWidths()
        {
            if (!isVanilla)
            {
                List<int> widths = Content.GetCustomManifestListInt("widths");
                if (widths != null)
                {
                    tableLayoutPanel2.Size = new Size(widths[0], tableLayoutPanel2.Size.Height);
                    tableLayoutPanel3.Size = new Size(widths[1], tableLayoutPanel3.Size.Height);
                    tableLayoutPanel4.Size = new Size(widths[2], tableLayoutPanel4.Size.Height);
                    tableLayoutPanel5.Size = new Size(widths[3], tableLayoutPanel5.Size.Height);
                    tableLayoutPanel6.Size = new Size(widths[4], tableLayoutPanel6.Size.Height);
                    tableLayoutPanel7.Size = new Size(widths[5], tableLayoutPanel7.Size.Height);
                    tableLayoutPanel8.Size = new Size(widths[6], tableLayoutPanel8.Size.Height);
                }
            }
            else
            {   // This part looks way uglier than the part above because I didn't make getter functions for the Settings, only Custom Manifests :(
                List<int> widths = Settings.settings.ContainsKey("widths") ? Settings.settings["widths"].ToObject<List<int>>() : null;
                if (widths != null)
                {
                    tableLayoutPanel2.Size = new Size(widths[0], tableLayoutPanel2.Size.Height);
                    tableLayoutPanel3.Size = new Size(widths[1], tableLayoutPanel3.Size.Height);
                    tableLayoutPanel4.Size = new Size(widths[2], tableLayoutPanel4.Size.Height);
                    tableLayoutPanel5.Size = new Size(widths[3], tableLayoutPanel5.Size.Height);
                    tableLayoutPanel6.Size = new Size(widths[4], tableLayoutPanel6.Size.Height);
                    tableLayoutPanel7.Size = new Size(widths[5], tableLayoutPanel7.Size.Height);
                    tableLayoutPanel8.Size = new Size(widths[6], tableLayoutPanel8.Size.Height);
                }
            }
        }

        public void SaveWidths()
        {
            if (!isVanilla)
            {
                Content.SetCustomManifestProperty("widths", new List<int>() {
                        tableLayoutPanel2.Size.Width,
                        tableLayoutPanel3.Size.Width,
                        tableLayoutPanel4.Size.Width,
                        tableLayoutPanel5.Size.Width,
                        tableLayoutPanel6.Size.Width,
                        tableLayoutPanel7.Size.Width,
                        tableLayoutPanel8.Size.Width,
                    });
                SaveCustomManifest(Content.currentDirectory);
            }
            else
            {
                Settings.settings["widths"] = JToken.FromObject(new List<int>() {
                        tableLayoutPanel2.Size.Width,
                        tableLayoutPanel3.Size.Width,
                        tableLayoutPanel4.Size.Width,
                        tableLayoutPanel5.Size.Width,
                        tableLayoutPanel6.Size.Width,
                        tableLayoutPanel7.Size.Width,
                        tableLayoutPanel8.Size.Width,
                    });
                Settings.SaveSettings();
            }
        }

        public void LoadFile(FileStream file, string filePath)
        {
            string fileText = new StreamReader(file).ReadToEnd();
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (fileText != "" && fileText != null)
            {
                JToken parsedJToken = JsonConvert.DeserializeObject<JObject>(fileText).First;
                string fileType = parsedJToken.Path;
                switch (fileType)
                {
                    case "elements":
                        foreach (JToken element in parsedJToken.First.ToArray())
                        {
                            if (element["xtriggers"] != null)
                            {
                                foreach (JProperty xtrigger in element["xtriggers"])
                                {
                                    if (xtrigger.Value as JArray != null) xtrigger.Value = xtrigger.Value as JArray;
                                    else if (xtrigger.Value as JObject != null) xtrigger.Value = new JArray(xtrigger.Value);
                                    else if (xtrigger.Value.Value<string>() != null) xtrigger.Value = JArray.FromObject(new List<XTrigger>() { new XTrigger(xtrigger.Value.Value<string>()) });
                                }
                            }

                            if (element["isAspect"] != null)
                            {
                                Aspect deserializedAspect = element.ToObject<Aspect>();
                                if (!Content.Aspects.ContainsKey(deserializedAspect.id))
                                {
                                    Content.Aspects.Add(deserializedAspect.id, deserializedAspect);
                                    ListViewItem aspectLVI = new ListViewItem(deserializedAspect.id)
                                    {
                                        Tag = deserializedAspect.GetHashCode()
                                    };
                                    aspectsListView.Items.Add(aspectLVI);
                                    if (aspectsListView.Groups[fileName] == null)
                                    {
                                        ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                        aspectsListView.Groups.Add(listViewGroup);
                                        aspectLVI.Group = listViewGroup;
                                    }
                                    else
                                    {
                                        aspectLVI.Group = aspectsListView.Groups[fileName];
                                    }
                                    deserializedAspect.filename = fileName;
                                }
                            }
                            else if (element["extends"] != null && Utilities.AspectExists(element["id"].ToString()))
                            {
                                Aspect deserializedAspect = element.ToObject<Aspect>();
                                if (!Content.Aspects.ContainsKey(deserializedAspect.id))
                                {
                                    Content.Aspects.Add(deserializedAspect.id, deserializedAspect);
                                    ListViewItem aspectLVI = new ListViewItem(deserializedAspect.id)
                                    {
                                        Tag = deserializedAspect.GetHashCode()
                                    };
                                    aspectsListView.Items.Add(aspectLVI);
                                    if (aspectsListView.Groups[fileName] == null)
                                    {
                                        ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                        aspectsListView.Groups.Add(listViewGroup);
                                        aspectLVI.Group = listViewGroup;
                                    }
                                    else
                                    {
                                        aspectLVI.Group = aspectsListView.Groups[fileName];
                                    }
                                    deserializedAspect.filename = fileName;
                                }
                            }
                            else
                            {
                                Element deserializedElement = element.ToObject<Element>();
                                if (!Content.Elements.ContainsKey(deserializedElement.id))
                                {
                                    Content.Elements.Add(deserializedElement.id, deserializedElement);
                                    ListViewItem elementLVI = new ListViewItem(deserializedElement.id)
                                    {
                                        Tag = deserializedElement.GetHashCode()
                                    };
                                    elementsListView.Items.Add(elementLVI);
                                    if (elementsListView.Groups[fileName] == null)
                                    {
                                        ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                        elementsListView.Groups.Add(listViewGroup);
                                        elementLVI.Group = listViewGroup;
                                    }
                                    else
                                    {
                                        elementLVI.Group = elementsListView.Groups[fileName];
                                    }
                                    deserializedElement.filename = fileName;
                                }
                            }
                        }
                        return;
                    case "recipes":
                        foreach (JToken recipe in parsedJToken.First.ToArray())
                        {
                            Recipe deserializedRecipe = recipe.ToObject<Recipe>();
                            if (!Content.Recipes.ContainsKey(deserializedRecipe.id))
                            {
                                Content.Recipes.Add(deserializedRecipe.id, deserializedRecipe);
                                ListViewItem recipeLVI = new ListViewItem(deserializedRecipe.id)
                                {
                                    Tag = deserializedRecipe.GetHashCode()
                                };
                                recipesListView.Items.Add(recipeLVI);
                                if (recipesListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    recipesListView.Groups.Add(listViewGroup);
                                    recipeLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    recipeLVI.Group = recipesListView.Groups[fileName];
                                }
                                deserializedRecipe.filename = fileName;
                            }
                        }
                        return;
                    case "decks":
                        foreach (JToken deck in parsedJToken.First.ToArray())
                        {
                            Deck deserializedDeck = deck.ToObject<Deck>();
                            if (!Content.Decks.ContainsKey(deserializedDeck.id))
                            {
                                Content.Decks.Add(deserializedDeck.id, deserializedDeck);
                                ListViewItem deckLVI = new ListViewItem(deserializedDeck.id)
                                {
                                    Tag = deserializedDeck.GetHashCode()
                                };
                                decksListView.Items.Add(deckLVI);
                                if (decksListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    decksListView.Groups.Add(listViewGroup);
                                    deckLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    deckLVI.Group = decksListView.Groups[fileName];
                                }
                                deserializedDeck.filename = fileName;
                            }
                        }
                        return;
                    case "legacies":
                        foreach (JToken legacy in parsedJToken.First.ToArray())
                        {
                            Legacy deserializedLegacy = legacy.ToObject<Legacy>();
                            if (!Content.Legacies.ContainsKey(deserializedLegacy.id))
                            {
                                Content.Legacies.Add(deserializedLegacy.id, deserializedLegacy);
                                ListViewItem legacyLVI = new ListViewItem(deserializedLegacy.id)
                                {
                                    Tag = deserializedLegacy.GetHashCode()
                                };
                                legaciesListView.Items.Add(legacyLVI);
                                if (legaciesListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    legaciesListView.Groups.Add(listViewGroup);
                                    legacyLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    legacyLVI.Group = legaciesListView.Groups[fileName];
                                }
                                deserializedLegacy.filename = fileName;
                            }
                        }
                        return;
                    case "endings":
                        foreach (JToken ending in parsedJToken.First.ToArray())
                        {
                            Ending deserializedEnding = ending.ToObject<Ending>();
                            if (!Content.Endings.ContainsKey(deserializedEnding.id))
                            {
                                Content.Endings.Add(deserializedEnding.id, deserializedEnding);
                                ListViewItem endingLVI = new ListViewItem(deserializedEnding.id)
                                {
                                    Tag = deserializedEnding.GetHashCode()
                                };
                                endingsListView.Items.Add(endingLVI);
                                if (endingsListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    endingsListView.Groups.Add(listViewGroup);
                                    endingLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    endingLVI.Group = endingsListView.Groups[fileName];
                                }
                                deserializedEnding.filename = fileName;
                            }
                        }
                        return;
                    case "verbs":
                        foreach (JToken verb in parsedJToken.First.ToArray())
                        {
                            Verb deserializedVerb = verb.ToObject<Verb>();
                            if (!Content.Verbs.ContainsKey(deserializedVerb.id))
                            {
                                Content.Verbs.Add(deserializedVerb.id, deserializedVerb);
                                ListViewItem verbLVI = new ListViewItem(deserializedVerb.id)
                                {
                                    Tag = deserializedVerb.GetHashCode()
                                };
                                verbsListView.Items.Add(verbLVI);
                                if (verbsListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    verbsListView.Groups.Add(listViewGroup);
                                    verbLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    verbLVI.Group = verbsListView.Groups[fileName];
                                }
                                deserializedVerb.filename = fileName;
                            }
                        }
                        return;
                    case "cultures":
                        foreach (JToken culture in parsedJToken.First.ToArray())
                        {
                            Culture deserializedCulture = culture.ToObject<Culture>();
                            if (!Content.Cultures.ContainsKey(deserializedCulture.id))
                            {
                                Content.Cultures.Add(deserializedCulture.id, deserializedCulture);
                            }
                        }
                        return;
                    default:
                        break;
                }
            }
        }

        private void CreateDirectories(string modLocation)
        {
            if (!Directory.Exists(modLocation + "/content/"))
            {
                Directory.CreateDirectory(modLocation + "/content/");
            }
            if (!Directory.Exists(modLocation + "/images/elements/"))
            {
                Directory.CreateDirectory(modLocation + "/images/elements/");
            }
            if (!Directory.Exists(modLocation + "/images/elements/anim/"))
            {
                Directory.CreateDirectory(modLocation + "/images/elements/anim/");
            }
            if (!Directory.Exists(modLocation + "/images/endings/"))
            {
                Directory.CreateDirectory(modLocation + "/images/endings/");
            }
            if (!Directory.Exists(modLocation + "/images/aspects/"))
            {
                Directory.CreateDirectory(modLocation + "/images/aspects/");
            }
            if (!Directory.Exists(modLocation + "/images/legacies/"))
            {
                Directory.CreateDirectory(modLocation + "/images/legacies/");
            }
            if (!Directory.Exists(modLocation + "/images/verbs/"))
            {
                Directory.CreateDirectory(modLocation + "/images/verbs/");
            }
            if (!Directory.Exists(modLocation + "/images/verbs/anim/"))
            {
                Directory.CreateDirectory(modLocation + "/images/verbs/anim/");
            }
            if (!Directory.Exists(modLocation + "/images/statusbaricons/"))
            {
                Directory.CreateDirectory(modLocation + "/images/statusbaricons/");
            }
            if (!Directory.Exists(modLocation + "/images/ui/"))
            {
                Directory.CreateDirectory(modLocation + "/images/ui/");
            }
        }

        public void SaveMod()
        {
            SaveMod(Content.currentDirectory);
        }

        public void SaveMod(string location)
        {
            CreateDirectories(location);
            if (Content.Aspects.Count > 0)
            {
                foreach (ListViewGroup group in aspectsListView.Groups)
                {
                    string fileName = group.Name;
                    List<Aspect> aspectsForGroup = new List<Aspect>();
                    foreach (ListViewItem item in group.Items)
                    {
                        // TODO switch below to using Tags
                        aspectsForGroup.Add(Content.Aspects[item.Text]);
                    }
                    if (aspectsForGroup.Count > 0)
                    {
                        JObject aspects = new JObject
                        {
                            ["elements"] = JArray.FromObject(aspectsForGroup)
                        };
                        string serializedAspects = JsonConvert.SerializeObject(aspects, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + fileName + ".json", FileMode.Create))))
                        {
                            jtw.WriteRaw(serializedAspects);
                        }
                    }
                    else
                    {
                        if (File.Exists(location + "/content/" + fileName + ".json"))
                        {
                            File.Delete(location + "/content/" + fileName + ".json");
                        }
                    }
                }
            }
            if (Content.Elements.Count > 0)
            {
                foreach (ListViewGroup group in elementsListView.Groups)
                {
                    string fileName = group.Name;
                    List<Element> elementsForGroup = new List<Element>();
                    foreach (ListViewItem item in group.Items)
                    {
                        // TODO switch below to using Tags
                        elementsForGroup.Add(Content.Elements[item.Text]);
                    }
                    if (elementsForGroup.Count > 0)
                    {
                        JObject elements = new JObject
                        {
                            ["elements"] = JArray.FromObject(elementsForGroup)
                        };
                        string serializedElements = JsonConvert.SerializeObject(elements, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + fileName + ".json", FileMode.Create))))
                        {
                            jtw.WriteRaw(serializedElements);
                        }
                    }
                    else
                    {
                        if (File.Exists(location + "/content/" + fileName + ".json"))
                        {
                            File.Delete(location + "/content/" + fileName + ".json");
                        }
                    }
                }
            }
            if (Content.Recipes.Count > 0)
            {
                foreach (ListViewGroup group in recipesListView.Groups)
                {
                    string fileName = group.Name;
                    List<Recipe> recipesForGroup = new List<Recipe>();
                    foreach (ListViewItem item in group.Items)
                    {
                        // TODO switch below to using Tags
                        recipesForGroup.Add(Content.Recipes[item.Text]);
                    }
                    if (recipesForGroup.Count > 0)
                    {
                        JObject recipes = new JObject
                        {
                            ["recipes"] = JArray.FromObject(recipesForGroup)
                        };
                        string serializedRecipes = JsonConvert.SerializeObject(recipes, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + fileName + ".json", FileMode.Create))))
                        {
                            jtw.WriteRaw(serializedRecipes);
                        }
                    }
                    else
                    {
                        if (File.Exists(location + "/content/" + fileName + ".json"))
                        {
                            File.Delete(location + "/content/" + fileName + ".json");
                        }
                    }
                }
            }
            if (decksListView.Items.Count > 0)
            {
                foreach (ListViewGroup group in decksListView.Groups)
                {
                    string fileName = group.Name;
                    List<Deck> decksForGroup = new List<Deck>();
                    foreach (ListViewItem item in group.Items)
                    {
                        // TODO switch below to using Tags
                        decksForGroup.Add(Content.Decks[item.Text]);
                    }
                    if (decksForGroup.Count > 0)
                    {
                        JObject decks = new JObject
                        {
                            ["decks"] = JArray.FromObject(decksForGroup)
                        };
                        string serializedDecks = JsonConvert.SerializeObject(decks, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + fileName + ".json", FileMode.Create))))
                        {
                            jtw.WriteRaw(serializedDecks);
                        }
                    }
                    else
                    {
                        if (File.Exists(location + "/content/" + fileName + ".json"))
                        {
                            File.Delete(location + "/content/" + fileName + ".json");
                        }
                    }
                }
            }
            if (legaciesListView.Items.Count > 0)
            {
                foreach (ListViewGroup group in legaciesListView.Groups)
                {
                    string fileName = group.Name;
                    List<Legacy> legaciesForGroup = new List<Legacy>();
                    foreach (ListViewItem item in group.Items)
                    {
                        // TODO switch below to using Tags
                        legaciesForGroup.Add(Content.Legacies[item.Text]);
                    }
                    if (legaciesForGroup.Count > 0)
                    {
                        JObject legacies = new JObject
                        {
                            ["legacies"] = JArray.FromObject(legaciesForGroup)
                        };
                        string serializedLegacies = JsonConvert.SerializeObject(legacies, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + fileName + ".json", FileMode.Create))))
                        {
                            jtw.WriteRaw(serializedLegacies);
                        }
                    }
                    else
                    {
                        if (File.Exists(location + "/content/" + fileName + ".json"))
                        {
                            File.Delete(location + "/content/" + fileName + ".json");
                        }
                    }
                }
            }
            if (endingsListView.Items.Count > 0)
            {
                foreach (ListViewGroup group in endingsListView.Groups)
                {
                    string fileName = group.Name;
                    List<Ending> endingsForGroup = new List<Ending>();
                    foreach (ListViewItem item in group.Items)
                    {
                        // TODO switch below to using Tags
                        endingsForGroup.Add(Content.Endings[item.Text]);
                    }
                    if (endingsForGroup.Count > 0)
                    {
                        JObject endings = new JObject
                        {
                            ["endings"] = JArray.FromObject(endingsForGroup)
                        };
                        string serializedLegacies = JsonConvert.SerializeObject(endings, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + fileName + ".json", FileMode.Create))))
                        {
                            jtw.WriteRaw(serializedLegacies);
                        }
                    }
                    else
                    {
                        if (File.Exists(location + "/content/" + fileName + ".json"))
                        {
                            File.Delete(location + "/content/" + fileName + ".json");
                        }
                    }
                }
            }
            if (verbsListView.Items.Count > 0)
            {
                foreach (ListViewGroup group in verbsListView.Groups)
                {
                    string fileName = group.Name;
                    List<Verb> verbsForGroup = new List<Verb>();
                    foreach (ListViewItem item in group.Items)
                    {
                        // TODO switch below to using Tags
                        verbsForGroup.Add(Content.Verbs[item.Text]);
                    }
                    if (verbsForGroup.Count > 0)
                    {
                        JObject verbs = new JObject
                        {
                            ["verbs"] = JArray.FromObject(verbsForGroup)
                        };
                        string serializedLegacies = JsonConvert.SerializeObject(verbs, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + fileName + ".json", FileMode.Create))))
                        {
                            jtw.WriteRaw(serializedLegacies);
                        }
                    }
                    else
                    {
                        if (File.Exists(location + "/content/" + fileName + ".json"))
                        {
                            File.Delete(location + "/content/" + fileName + ".json");
                        }
                    }
                }
            }
            if (Content.Cultures.Count > 0)
            {
                // TODO fix this, I'm *pretty* sure it doesn't save cultures correctly. It looks like they need to be saved individually, in subfolders, with a blank json file named after the culture's ID?
                JObject cultures = new JObject
                {
                    ["cultures"] = JArray.FromObject(Content.Cultures.Values)
                };
                string culturesJson = JsonConvert.SerializeObject(cultures, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/cultures.json", FileMode.Create))))
                {
                    jtw.WriteRaw(culturesJson);
                }
            }
            else
            {
                if (File.Exists(location + "/content/cultures.json"))
                {
                    File.Delete(location + "/content/cultures.json");
                }
            }
            SaveManifests(location);
        }

        public void SaveManifests(string location)
        {
            if (isVanilla) return;
            string synopsisJson = JsonConvert.SerializeObject(Content.synopsis, Formatting.Indented);
            using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/synopsis.json", FileMode.Create))))
            {
                jtw.WriteRaw(synopsisJson);
            }
            SaveCustomManifest(location);
        }

        private void SaveCustomManifest(string location)
        {
            if (isVanilla) return;
            if (Content.CustomManifest.Count > 0)
            {
                string CustomManifestJson = JsonConvert.SerializeObject(Content.CustomManifest, Formatting.Indented);
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/CarcassSpark.Manifest.json", FileMode.Create))))
                {
                    jtw.WriteRaw(CustomManifestJson);
                }
            }
            else if (File.Exists(location + "/CarcassSpark.Manifest.json"))
            {
                File.Delete(location + "/CarcassSpark.Manifest.json");
            }
        }

        private void AspectListView_DoubleClick(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            if (editMode)
            {
                AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), AspectsList_Assign);
                av.Show();
            }
            else
            {
                AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), null);
                av.Show();
            }
        }

        private void AspectsList_Assign(object sender, Aspect result)
        {
            Content.Aspects[result.id] = result.Copy();
            if (aspectsListView.Items[aspectsListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Aspects.Remove(aspectsListView.SelectedItems[0].Text);
                aspectsListView.SelectedItems[0].Text = result.id;
                aspectsListView.SelectedItems[0].Tag = result.GetHashCode();
            }
        }

        private void DecksListView_DoubleClick(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            string id = decksListView.SelectedItems[0].Text;
            if (editMode)
            {
                DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), DecksList_Assign);
                dv.Show();
            }
            else
            {
                DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), null);
                dv.Show();
            }
        }

        private void DecksList_Assign(object sender, Deck result)
        {
            Content.Decks[result.id] = result.Copy();
            if (decksListView.Items[decksListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Decks.Remove(decksListView.SelectedItems[0].Text);
                decksListView.SelectedItems[0].Text = result.id;
                decksListView.SelectedItems[0].Tag = result.GetHashCode();
            }
        }

        private void ElementsListView_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            if (editMode)
            {
                ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), ElementsList_Assign);
                ev.Show();
            }
            else
            {
                ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), null);
                ev.Show();
            }
        }

        private void ElementsList_Assign(object sender, Element result)
        {
            Content.Elements[result.id] = result.Copy();
            if (elementsListView.Items[elementsListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Elements.Remove(elementsListView.SelectedItems[0].Text);
                elementsListView.SelectedItems[0].Text = result.id;
                elementsListView.SelectedItems[0].Tag = result.GetHashCode();
            }
        }

        private void EndingsListView_DoubleClick(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1) return;
            string id = endingsListView.SelectedItems[0].Text;
            if (editMode)
            {
                EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), EndingsList_Assign);
                ev.Show();
            }
            else
            {
                EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), null);
                ev.Show();
            }
        }

        private void EndingsList_Assign(object sender, Ending result)
        {
            Content.Endings[result.id] = result.Copy();
            if (endingsListView.Items[endingsListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Endings.Remove(endingsListView.SelectedItems[0].Text);
                endingsListView.SelectedItems[0].Text = result.id;
                endingsListView.SelectedItems[0].Tag = result.GetHashCode();
            }
        }

        private void LegaciesListView_DoubleClick(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1) return;
            string id = legaciesListView.SelectedItems[0].Text;
            if (editMode)
            {
                LegacyViewer lv = new LegacyViewer(Content.GetLegacy(id).Copy(), LegaciesList_Assign);
                lv.Show();
            }
            else
            {
                LegacyViewer lv = new LegacyViewer(Content.GetLegacy(id).Copy(), null);
                lv.Show();
            }
        }

        private void LegaciesList_Assign(object sender, Legacy result)
        {
            Content.Legacies[result.id] = result.Copy();
            if (legaciesListView.Items[legaciesListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Legacies.Remove(legaciesListView.SelectedItems[0].Text);
                legaciesListView.SelectedItems[0].Text = result.id;
                legaciesListView.SelectedItems[0].Tag = result.GetHashCode();
            }
        }

        private void RecipesListView_DoubleClick(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            string id = recipesListView.SelectedItems[0].Text;
            if (editMode)
            {
                RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), RecipesList_Assign);
                rv.Show();
            }
            else
            {
                RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), null);
                rv.Show();
            }
        }

        private void RecipesList_Assign(object sender, Recipe result)
        {
            Content.Recipes[result.id] = result.Copy();
            if (recipesListView.Items[recipesListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Recipes.Remove(recipesListView.SelectedItems[0].Text);
                recipesListView.SelectedItems[0].Text = result.id;
                recipesListView.SelectedItems[0].Tag = result.GetHashCode();
            }
        }

        private void VerbsListView_DoubleClick(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1) return;
            string id = verbsListView.SelectedItems[0].Text;
            if (editMode)
            {
                VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), VerbsList_Assign);
                vv.Show();
            }
            else
            {
                VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), null);
                vv.Show();
            }
        }

        private void VerbsList_Assign(object sender, Verb result)
        {
            Content.Verbs[result.id] = result.Copy();
            if (verbsListView.Items[verbsListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Verbs.Remove(verbsListView.SelectedItems[0].Text);
                verbsListView.SelectedItems[0].Text = result.id;
                verbsListView.SelectedItems[0].Tag = result.GetHashCode();
            }
        }

        private void AspectsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            aspectsListView.BeginUpdate();
            if (aspectsSearchTextBox.Text != "")
            {
                if (aspectsSearchTextBox.Text.Length >= 3)
                {
                    Aspect[] aspectsToAdd = SearchAspects(Content.Aspects.Values.ToList(), aspectsSearchTextBox.Text);
                    List<ListViewItem> items = new List<ListViewItem>();
                    aspectsListView.Items.Clear();
                    foreach (Aspect aspect in aspectsToAdd)
                    {
                        ListViewGroup group;
                        if (aspectsListView.Groups[aspect.filename] == null)
                        {
                            group = new ListViewGroup(aspect.filename, aspect.filename);
                        }
                        else
                        {
                            group = aspectsListView.Groups[aspect.filename];
                        }
                        ListViewItem item = new ListViewItem(aspect.id) { Tag = aspect.GetHashCode() };
                        group.Items.Add(item);
                        aspectsListView.Items.Add(item);
                    }
                }
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                aspectsListView.Items.Clear();
                foreach (Aspect aspect in Content.Aspects.Values)
                {
                    ListViewGroup group;
                    if (aspectsListView.Groups[aspect.filename] == null)
                    {
                        group = new ListViewGroup(aspect.filename, aspect.filename);
                    }
                    else
                    {
                        group = aspectsListView.Groups[aspect.filename];
                    }
                    ListViewItem item = new ListViewItem(aspect.id) { Tag = aspect.GetHashCode() };
                    group.Items.Add(item);
                    aspectsListView.Items.Add(item);
                }
            }
            aspectsListView.EndUpdate();
        }

        private void ElementsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            elementsListView.BeginUpdate();
            if (elementsSearchTextBox.Text != "")
            {
                if (elementsSearchTextBox.Text.Length >= 3)
                {
                    Element[] elementsToAdd = SearchElements(Content.Elements.Values.ToList(), elementsSearchTextBox.Text);
                    List<ListViewItem> items = new List<ListViewItem>();
                    elementsListView.Items.Clear();
                    foreach (Element element in elementsToAdd)
                    {
                        ListViewGroup group;
                        if (elementsListView.Groups[element.filename] == null)
                        {
                            group = new ListViewGroup(element.filename, element.filename);
                        }
                        else
                        {
                            group = elementsListView.Groups[element.filename];
                        }
                        ListViewItem item = new ListViewItem(element.id) { Tag = element.GetHashCode() };
                        group.Items.Add(item);
                        elementsListView.Items.Add(item);
                    }
                }
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                elementsListView.Items.Clear();
                foreach (Element element in Content.Elements.Values)
                {
                    ListViewGroup group;
                    if (elementsListView.Groups[element.filename] == null)
                    {
                        group = new ListViewGroup(element.filename, element.filename);
                    }
                    else
                    {
                        group = elementsListView.Groups[element.filename];
                    }
                    ListViewItem item = new ListViewItem(element.id) { Tag = element.GetHashCode() };
                    group.Items.Add(item);
                    elementsListView.Items.Add(item);
                }
            }
            elementsListView.EndUpdate();
        }

        private void RecipesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            recipesListView.BeginUpdate();
            if (recipesSearchTextBox.Text != "")
            {
                if (recipesSearchTextBox.Text.Length >= 3)
                {
                    Recipe[] recipesToAdd = SearchRecipes(Content.Recipes.Values.ToList(), recipesSearchTextBox.Text);
                    List<ListViewItem> items = new List<ListViewItem>();
                    recipesListView.Items.Clear();
                    foreach (Recipe recipe in recipesToAdd)
                    {
                        ListViewGroup group;
                        if (recipesListView.Groups[recipe.filename] == null)
                        {
                            group = new ListViewGroup(recipe.filename, recipe.filename);
                        }
                        else
                        {
                            group = recipesListView.Groups[recipe.filename];
                        }
                        ListViewItem item = new ListViewItem(recipe.id) { Tag = recipe.GetHashCode() };
                        group.Items.Add(item);
                        recipesListView.Items.Add(item);
                    }
                }
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                recipesListView.Items.Clear();
                foreach (Recipe recipe in Content.Recipes.Values)
                {
                    ListViewGroup group;
                    if (recipesListView.Groups[recipe.filename] == null)
                    {
                        group = new ListViewGroup(recipe.filename, recipe.filename);
                    }
                    else
                    {
                        group = recipesListView.Groups[recipe.filename];
                    }
                    ListViewItem item = new ListViewItem(recipe.id) { Tag = recipe.GetHashCode() };
                    group.Items.Add(item);
                    recipesListView.Items.Add(item);
                }
            }
            recipesListView.EndUpdate();
        }

        private void DecksSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            decksListView.BeginUpdate();
            if (decksSearchTextBox.Text != "")
            {
                if (decksSearchTextBox.Text.Length >= 3)
                {
                    Deck[] decksToAdd = SearchDecks(Content.Decks.Values.ToList(), decksSearchTextBox.Text);
                    List<ListViewItem> items = new List<ListViewItem>();
                    decksListView.Items.Clear();
                    foreach (Deck deck in decksToAdd)
                    {
                        ListViewGroup group;
                        if (decksListView.Groups[deck.filename] == null)
                        {
                            group = new ListViewGroup(deck.filename, deck.filename);
                        }
                        else
                        {
                            group = decksListView.Groups[deck.filename];
                        }
                        ListViewItem item = new ListViewItem(deck.id) { Tag = deck.GetHashCode() };
                        group.Items.Add(item);
                        decksListView.Items.Add(item);
                    }
                }
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                decksListView.Items.Clear();
                foreach (Deck deck in Content.Decks.Values)
                {
                    ListViewGroup group;
                    if (decksListView.Groups[deck.filename] == null)
                    {
                        group = new ListViewGroup(deck.filename, deck.filename);
                    }
                    else
                    {
                        group = decksListView.Groups[deck.filename];
                    }
                    ListViewItem item = new ListViewItem(deck.id) { Tag = deck.GetHashCode() };
                    group.Items.Add(item);
                    decksListView.Items.Add(item);
                }
            }
            decksListView.EndUpdate();
        }

        private void LegaciesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            legaciesListView.BeginUpdate();
            if (legaciesSearchTextBox.Text != "")
            {
                if (legaciesSearchTextBox.Text.Length >= 3)
                {
                    Legacy[] legaciesToAdd = SearchLegacies(Content.Legacies.Values.ToList(), legaciesSearchTextBox.Text);
                    List<ListViewItem> items = new List<ListViewItem>();
                    legaciesListView.Items.Clear();
                    foreach (Legacy legacy in legaciesToAdd)
                    {
                        ListViewGroup group;
                        if (legaciesListView.Groups[legacy.filename] == null)
                        {
                            group = new ListViewGroup(legacy.filename, legacy.filename);
                        }
                        else
                        {
                            group = legaciesListView.Groups[legacy.filename];
                        }
                        ListViewItem item = new ListViewItem(legacy.id) { Tag = legacy.GetHashCode() };
                        group.Items.Add(item);
                        legaciesListView.Items.Add(item);
                    }
                }
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                legaciesListView.Items.Clear();
                foreach (Legacy legacy in Content.Legacies.Values)
                {
                    ListViewGroup group;
                    if (legaciesListView.Groups[legacy.filename] == null)
                    {
                        group = new ListViewGroup(legacy.filename, legacy.filename);
                    }
                    else
                    {
                        group = legaciesListView.Groups[legacy.filename];
                    }
                    ListViewItem item = new ListViewItem(legacy.id) { Tag = legacy.GetHashCode() };
                    group.Items.Add(item);
                    legaciesListView.Items.Add(item);
                }
            }
            legaciesListView.EndUpdate();
        }

        private void EndingsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            endingsListView.BeginUpdate();
            if (endingsSearchTextBox.Text != "")
            {
                if (endingsSearchTextBox.Text.Length >= 3)
                {
                    Ending[] endingsToAdd = SearchEndings(Content.Endings.Values.ToList(), endingsSearchTextBox.Text);
                    List<ListViewItem> items = new List<ListViewItem>();
                    endingsListView.Items.Clear();
                    foreach (Ending ending in endingsToAdd)
                    {
                        ListViewGroup group;
                        if (endingsListView.Groups[ending.filename] == null)
                        {
                            group = new ListViewGroup(ending.filename, ending.filename);
                        }
                        else
                        {
                            group = endingsListView.Groups[ending.filename];
                        }
                        ListViewItem item = new ListViewItem(ending.id) { Tag = ending.GetHashCode() };
                        group.Items.Add(item);
                        endingsListView.Items.Add(item);
                    }
                }
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                endingsListView.Items.Clear();
                foreach (Ending ending in Content.Endings.Values)
                {
                    ListViewGroup group;
                    if (endingsListView.Groups[ending.filename] == null)
                    {
                        group = new ListViewGroup(ending.filename, ending.filename);
                    }
                    else
                    {
                        group = endingsListView.Groups[ending.filename];
                    }
                    ListViewItem item = new ListViewItem(ending.id) { Tag = ending.GetHashCode() };
                    group.Items.Add(item);
                    endingsListView.Items.Add(item);
                }
            }
            endingsListView.EndUpdate();
        }

        private void VerbsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            verbsListView.BeginUpdate();
            if (verbsSearchTextBox.Text != "")
            {
                if (verbsSearchTextBox.Text.Length >= 3)
                {
                    Verb[] verbsToAdd = SearchVerbs(Content.Verbs.Values.ToList(), verbsSearchTextBox.Text);
                    List<ListViewItem> items = new List<ListViewItem>();
                    verbsListView.Items.Clear();
                    foreach (Verb verb in verbsToAdd)
                    {
                        ListViewGroup group;
                        if (verbsListView.Groups[verb.filename] == null)
                        {
                            group = new ListViewGroup(verb.filename, verb.filename);
                        }
                        else
                        {
                            group = verbsListView.Groups[verb.filename];
                        }
                        ListViewItem item = new ListViewItem(verb.id) { Tag = verb.GetHashCode() };
                        group.Items.Add(item);
                        verbsListView.Items.Add(item);
                    }
                }
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                verbsListView.Items.Clear();
                foreach (Verb verb in Content.Verbs.Values)
                {
                    ListViewGroup group;
                    if (verbsListView.Groups[verb.filename] == null)
                    {
                        group = new ListViewGroup(verb.filename, verb.filename);
                    }
                    else
                    {
                        group = verbsListView.Groups[verb.filename];
                    }
                    ListViewItem item = new ListViewItem(verb.id) { Tag = verb.GetHashCode() };
                    group.Items.Add(item);
                    verbsListView.Items.Add(item);
                }
            }
            verbsListView.EndUpdate();
        }

        private string[] SearchKeys(List<string> keysList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from id in keysList
                    where regex.IsMatch(id)
                    select id).ToArray();
        }

        private Aspect[] SearchAspects(List<Aspect> aspectsList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from aspect in aspectsList
                    where (aspect.id != null && regex.IsMatch(aspect.id))
                       || (aspect.label != null && regex.IsMatch(aspect.label))
                       || (aspect.description != null && regex.IsMatch(aspect.description))
                       || (aspect.comments != null && regex.IsMatch(aspect.comments))
                    select aspect).ToArray();
        }

        private Element[] SearchElements(List<Element> elementsList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from element in elementsList
                    where (element.id != null && regex.IsMatch(element.id))
                       || (element.label != null && regex.IsMatch(element.label))
                       || (element.comments != null && regex.IsMatch(element.comments))
                    select element).ToArray();
        }

        private Recipe[] SearchRecipes(List<Recipe> recipesList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from recipe in recipesList
                    where (recipe.id != null && regex.IsMatch(recipe.id))
                       || (recipe.label != null && regex.IsMatch(recipe.label)) 
                       || (recipe.description != null && regex.IsMatch(recipe.description))
                       || (recipe.startdescription != null && regex.IsMatch(recipe.startdescription))
                       || (recipe.comments != null && regex.IsMatch(recipe.comments))
                    select recipe).ToArray();
        }

        private Deck[] SearchDecks(List<Deck> decksList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from deck in decksList
                    where (deck.id != null && regex.IsMatch(deck.id))
                       || (deck.label != null && regex.IsMatch(deck.label))
                       || (deck.description != null && regex.IsMatch(deck.description))
                       || (deck.comments != null && regex.IsMatch(deck.comments))
                    select deck).ToArray();
        }

        private Legacy[] SearchLegacies(List<Legacy> recipesList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from legacy in recipesList
                    where (legacy.id != null && regex.IsMatch(legacy.id))
                       || (legacy.label != null && regex.IsMatch(legacy.label))
                       || (legacy.description != null && regex.IsMatch(legacy.description))
                       || (legacy.startdescription != null && regex.IsMatch(legacy.startdescription))
                       || (legacy.comments != null && regex.IsMatch(legacy.comments))
                    select legacy).ToArray();
        }

        private Ending[] SearchEndings(List<Ending> recipesList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from ending in recipesList
                    where (ending.id != null && regex.IsMatch(ending.id))
                       || (ending.label != null && regex.IsMatch(ending.label))
                       || (ending.description != null && regex.IsMatch(ending.description))
                       || (ending.comments != null && regex.IsMatch(ending.comments))
                    select ending).ToArray();
        }

        private Verb[] SearchVerbs(List<Verb> recipesList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from verb in recipesList
                    where (verb.id != null && regex.IsMatch(verb.id))
                       || (verb.label != null && regex.IsMatch(verb.label))
                       || (verb.description != null && regex.IsMatch(verb.description))
                       || (verb.comments != null && regex.IsMatch(verb.comments))
                    select verb).ToArray();
        }

        private void ElementsWithThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.aspects != null && element.aspects.ContainsKey(id))
                {
                    tmp.Add(element.id, element);
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void ElementsThatReactWithThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.xtriggers != null && element.xtriggers.ContainsKey(id))
                {
                    tmp.Add(element.id, element);
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void RecipesRequiringThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.requirements != null)
                {
                    if (recipe.requirements.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                    else if (recipe.requirements.ContainsValue(id)) tmp.Add(recipe.id, recipe);
                }
                else if (recipe.extantreqs != null)
                {
                    if (recipe.extantreqs.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                    else if (recipe.extantreqs.ContainsValue(id)) tmp.Add(recipe.id, recipe);
                }
                else if (recipe.tablereqs != null)
                {
                    if (recipe.tablereqs.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                    else if (recipe.tablereqs.ContainsValue(id)) tmp.Add(recipe.id, recipe);
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatProduceThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.aspects != null && (recipe.aspects.ContainsKey(id) && recipe.aspects[id] > 0))
                {
                    tmp.Add(recipe.id, recipe);
                }
                else if (recipe.effects != null && (recipe.effects.ContainsKey(id) && Convert.ToInt32(recipe.effects[id]) > 0))
                {
                    tmp.Add(recipe.id, recipe);
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }
        
        private void SlotsRequiringThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                // TODO foreach (Slot slot in element.slots) if (slot.requirements.contains(id)) tmp.add(element.id, element)
                foreach (Slot slot in element.slots)
                {
                    if (slot.required.ContainsKey(id)) tmp.Add(element.id, element);
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void ElementsThatDecayIntoThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.decayTo == id)
                {
                    tmp.Add(element.id, element);
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void ElementsThatXtriggerIntoThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.xtriggers != null)
                {
                    foreach (KeyValuePair<string, List<XTrigger>> xtrigger in element.xtriggers)
                    {
                        foreach (XTrigger xtriggereffect in xtrigger.Value)
                        {
                            if (xtriggereffect.id == id) tmp.Add(element.id, element);
                        }
                    }
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void RecipesThatRequireThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.requirements != null)
                {
                    if (recipe.requirements.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                }
                else if (recipe.extantreqs != null)
                {
                    if (recipe.extantreqs.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                }
                else if (recipe.tablereqs != null)
                {
                    if (recipe.tablereqs.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatProduceThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.effects != null && (recipe.effects.ContainsKey(id) || recipe.effects.ContainsValue(id)))
                {
                    tmp.Add(recipe.id, recipe);
                }
                else if (recipe.aspects != null && (recipe.aspects.ContainsKey(id) && recipe.aspects[id] > 0))
                {
                    tmp.Add(recipe.id, recipe);
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void DecksThatContainThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Deck> tmp = new Dictionary<string, Deck>();
            foreach (Deck deck in Content.Decks.Values)
            {
                if (deck.spec != null && deck.spec.Contains(id))
                {
                    tmp.Add(deck.id, deck);
                }
            }
            if (tmp.Count > 0)
            {
                DecksDictionaryResults ddr = new DecksDictionaryResults(tmp);
                ddr.Show();
            }
        }

        private void LegaciesThatStartWithThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Legacy> tmp = new Dictionary<string, Legacy>();
            foreach (Legacy legacy in Content.Legacies.Values)
            {
                if (legacy.effects != null && legacy.effects.ContainsKey(id))
                {
                    tmp.Add(legacy.id, legacy);
                }
            }
            if (tmp.Count > 0)
            {
                LegaciesDictionaryResults ldr = new LegaciesDictionaryResults(tmp);
                ldr.Show();
            }
        }

        private void RecipesThatLinkToThisRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            string id = recipesListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.linked != null) foreach (RecipeLink link in recipe.linked)
                    {
                        if (link.id == id)
                        {
                            tmp.Add(recipe.id, recipe);
                        }
                    }
                if (recipe.alt != null) foreach (RecipeLink link in recipe.alt)
                    {
                        if (link.id == id)
                        {
                            tmp.Add(recipe.id, recipe);
                        }
                    }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatDrawFromThisDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            string id = decksListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.deckeffects != null && recipe.deckeffects.ContainsKey(id) && recipe.deckeffects[id] > 0)
                {
                    tmp.Add(recipe.id, recipe);
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatCauseThisEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1) return;
            string id = endingsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.ending != null && recipe.ending == id)
                {
                    tmp.Add(recipe.id, recipe);
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatUseThisVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1) return;
            string id = verbsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.actionId != null && recipe.actionId == id)
                {
                    tmp.Add(recipe.id, recipe);
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void ElementsWithSlotsForThisVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1) return;
            string id = verbsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.slots != null) foreach (Slot slot in element.slots)
                    {
                        if (slot.actionId == id) tmp[element.id] = element;
                    }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void ViewAsFlowchartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            string id = recipesListView.SelectedItems[0].Text;
            RecipeFlowchartViewer rfv = new RecipeFlowchartViewer(Content.GetRecipe(id).Copy());
            rfv.Show();
        }

        private void SaveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToFolderBrowserDialog.SelectedPath = Content.currentDirectory;
            if (saveToFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SaveMod(saveToFolderBrowserDialog.SelectedPath);
            }
        }

        private void AutosaveTimer_Tick(object sender, EventArgs e)
        {
            SaveMod();
        }

        private void DeleteSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            ListViewItem listViewItem = aspectsListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                aspectsListView.Items.Remove(listViewItem);
                Content.Aspects.Remove(listViewItem.Text);
            }
        }

        private void DeleteSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            ListViewItem listViewItem = elementsListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                elementsListView.Items.Remove(listViewItem);
                Content.Elements.Remove(listViewItem.Text);
            }
        }

        private void DeleteSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            ListViewItem listViewItem = recipesListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                recipesListView.Items.Remove(listViewItem);
                Content.Recipes.Remove(listViewItem.Text);
            }
        }

        private void DeleteSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            ListViewItem listViewItem = decksListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                decksListView.Items.Remove(listViewItem);
                Content.Decks.Remove(listViewItem.Text);
            }
        }

        private void DeleteSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1) return;
            ListViewItem listViewItem = legaciesListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                legaciesListView.Items.Remove(listViewItem);
                Content.Legacies.Remove(listViewItem.Text);
            }
        }

        private void DeleteSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1) return;
            ListViewItem ListViewItem = endingsListView.SelectedItems[0];
            if (ConfirmDelete(ListViewItem.Text) == DialogResult.Yes)
            {
                endingsListView.Items.Remove(ListViewItem);
                Content.Endings.Remove(ListViewItem.Text);
            }
        }

        private void DeleteSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1) return;
            ListViewItem ListViewItem = verbsListView.SelectedItems[0];
            if (ConfirmDelete(ListViewItem.Text) == DialogResult.Yes)
            {
                verbsListView.Items.Remove(ListViewItem);
                Content.Verbs.Remove(ListViewItem.Text);
            }
        }

        public DialogResult ConfirmDelete(string id)
        {
            if (id == null) return MessageBox.Show("Are you sure you'd like to delete this item?", "Delete Item", MessageBoxButtons.YesNo);
            return MessageBox.Show("Are you sure you'd like to delete " + id + "?", "Delete Item", MessageBoxButtons.YesNo);
        }

        public void Deleted(string id)
        {
            MessageBox.Show(id + "has been deleted.");
        }

        private void AspectsListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                aspectsListView.SelectedIndices.Clear();
                aspectsListView.Select();
                Point point = aspectsListView.PointToClient(Cursor.Position);
                if (aspectsListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void ElementsListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                elementsListView.SelectedIndices.Clear();
                elementsListView.Select();
                Point point = elementsListView.PointToClient(Cursor.Position);
                if (elementsListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void RecipesListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                recipesListView.SelectedIndices.Clear();
                recipesListView.Select();
                Point point = recipesListView.PointToClient(Cursor.Position);
                if (recipesListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void DecksListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                decksListView.SelectedIndices.Clear();
                decksListView.Select();
                Point point = decksListView.PointToClient(Cursor.Position);
                if (decksListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void LegaciesListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                legaciesListView.SelectedIndices.Clear();
                legaciesListView.Select();
                Point point = legaciesListView.PointToClient(Cursor.Position);
                if (legaciesListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void EndingsListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                endingsListView.SelectedIndices.Clear();
                endingsListView.Select();
                Point point = endingsListView.PointToClient(Cursor.Position);
                if (endingsListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void VerbsListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                verbsListView.SelectedIndices.Clear();
                verbsListView.Select();
                Point point = verbsListView.PointToClient(Cursor.Position);
                if (verbsListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void OpenSelectedAspectsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            Aspect aspectToEdit = Content.GetAspect(aspectsListView.SelectedItems[0].Text);
            if (aspectToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(aspectToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(je.objectText);
                if (deserializedAspect.id != aspectsListView.SelectedItems[0].Text)
                {
                    Content.Aspects.Remove(aspectsListView.SelectedItems[0].Text);
                    Content.Aspects[deserializedAspect.id] = deserializedAspect.Copy();
                    aspectsListView.SelectedItems[0].Text = deserializedAspect.id;
                    aspectsListView.SelectedItems[0].Tag = deserializedAspect.GetHashCode();
                }
                else
                {
                    Content.Aspects[aspectsListView.SelectedItems[0].Text] = deserializedAspect.Copy();
                }
            }
        }

        private void OpenSelectedElementsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            Element elementToEdit = Content.GetElement(elementsListView.SelectedItems[0].Text);
            if (elementToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(elementToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Element deserializedElement = JsonConvert.DeserializeObject<Element>(je.objectText);
                if (deserializedElement.id != elementsListView.SelectedItems[0].Text)
                {
                    Content.Elements.Remove(elementsListView.SelectedItems[0].Text);
                    Content.Elements[deserializedElement.id] = deserializedElement.Copy();
                    elementsListView.SelectedItems[0].Text = deserializedElement.id;
                    elementsListView.SelectedItems[0].Tag = deserializedElement.GetHashCode();
                }
                else
                {
                    Content.Elements[elementsListView.SelectedItems[0].Text] = deserializedElement.Copy();
                }
            }
        }

        private void OpenSelectedRecipesJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            Recipe recipeToEdit = Content.GetRecipe(recipesListView.SelectedItems[0].Text);
            if (recipeToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(recipeToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(je.objectText);
                if (deserializedRecipe.id != recipesListView.SelectedItems[0].Text)
                {
                    Content.Recipes.Remove(recipesListView.SelectedItems[0].Text);
                    Content.Recipes[deserializedRecipe.id] = deserializedRecipe.Copy();
                    recipesListView.SelectedItems[0].Text = deserializedRecipe.id;
                    recipesListView.SelectedItems[0].Tag = deserializedRecipe.GetHashCode();
                }
                else
                {
                    Content.Recipes[recipesListView.SelectedItems[0].Text] = deserializedRecipe.Copy();
                }
            }
        }

        private void OpenSelectedDecksJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            Deck deckToEdit = Content.GetDeck(decksListView.SelectedItems[0].Text);
            if (deckToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(deckToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(je.objectText);
                if (deserializedDeck.id != decksListView.SelectedItems[0].Text)
                {
                    Content.Decks.Remove(decksListView.SelectedItems[0].Text);
                    Content.Decks[deserializedDeck.id] = deserializedDeck.Copy();
                    decksListView.SelectedItems[0].Text = deserializedDeck.id;
                    decksListView.SelectedItems[0].Tag = deserializedDeck.GetHashCode();
                }
                else
                {
                    Content.Decks[decksListView.SelectedItems[0].Text] = deserializedDeck.Copy();
                }
            }
        }

        private void OpenSelectedLegacysJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1) return;
            Legacy legacyToEdit = Content.GetLegacy(legaciesListView.SelectedItems[0].Text);
            if (legacyToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(legacyToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Legacy deserializedLegacy = JsonConvert.DeserializeObject<Legacy>(je.objectText);
                if (deserializedLegacy.id != legaciesListView.SelectedItems[0].Text)
                {
                    Content.Legacies.Remove(legaciesListView.SelectedItems[0].Text);
                    Content.Legacies[deserializedLegacy.id] = deserializedLegacy.Copy();
                    legaciesListView.SelectedItems[0].Text = deserializedLegacy.id;
                    legaciesListView.SelectedItems[0].Tag = deserializedLegacy.GetHashCode();
                }
                else
                {
                    Content.Legacies[legaciesListView.SelectedItems[0].Text] = deserializedLegacy.Copy();
                }
            }
        }

        private void OpenSelectedEndingsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1) return;
            Ending endingToEdit = Content.GetEnding(endingsListView.SelectedItems[0].Text);
            if (endingToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(endingToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Ending deserializedEnding = JsonConvert.DeserializeObject<Ending>(je.objectText);
                if (deserializedEnding.id != endingsListView.SelectedItems[0].Text)
                {
                    Content.Endings.Remove(endingsListView.SelectedItems[0].Text);
                    Content.Endings[deserializedEnding.id] = deserializedEnding.Copy();
                    endingsListView.SelectedItems[0].Text = deserializedEnding.id;
                    endingsListView.SelectedItems[0].Tag = deserializedEnding.GetHashCode();
                }
                else
                {
                    Content.Endings[endingsListView.SelectedItems[0].Text] = deserializedEnding.Copy();
                }
            }
        }

        private void OpenSelectedVerbsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1) return;
            Verb verbToEdit = Content.GetVerb(verbsListView.SelectedItems[0].Text);
            if (verbToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(verbToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Verb deserializedVerb = JsonConvert.DeserializeObject<Verb>(je.objectText);
                if (deserializedVerb.id != verbsListView.SelectedItems[0].Text)
                {
                    Content.Verbs.Remove(verbsListView.SelectedItems[0].Text);
                    Content.Verbs[deserializedVerb.id] = deserializedVerb.Copy();
                    verbsListView.SelectedItems[0].Text = deserializedVerb.id;
                }
                else
                {
                    Content.Verbs[verbsListView.SelectedItems[0].Text] = deserializedVerb.Copy();
                }
            }
        }

        private void DuplicateSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            Aspect newAspect = Content.Aspects[aspectsListView.SelectedItems[0].Text].Copy();
            string id = newAspect.id;
            if (aspectsListView.Items.ContainsKey(id))
            {
                id += "_";
                int tmp = 1;
                while (aspectsListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newAspect.id = id;
            ListViewItem newListViewItem = new ListViewItem(newAspect.id) { Tag = newAspect.GetHashCode() };
            aspectsListView.Groups[aspectsListView.SelectedItems[0].Group.Name].Items.Add(newListViewItem);
            aspectsListView.Items.Add(newListViewItem);
            Content.Aspects.Add(newAspect.id, newAspect.Copy());
        }

        private void DuplicateSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            Element newElement = Content.Elements[elementsListView.SelectedItems[0].Text].Copy();
            string id = newElement.id;
            if (elementsListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (elementsListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newElement.id = id;
            ListViewItem newListViewItem = new ListViewItem(newElement.id) { Tag = newElement.GetHashCode() };
            elementsListView.Groups[elementsListView.SelectedItems[0].Group.Name].Items.Add(newListViewItem);
            elementsListView.Items.Add(newListViewItem);
            Content.Elements.Add(newElement.id, newElement.Copy());
        }

        private void DuplicateSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            Recipe newRecipe = Content.Recipes[recipesListView.SelectedItems[0].Text].Copy();
            string id = newRecipe.id;
            if (recipesListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (recipesListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newRecipe.id = id;
            ListViewItem newListViewItem = new ListViewItem(newRecipe.id) { Tag = newRecipe.GetHashCode() };
            recipesListView.Groups[recipesListView.SelectedItems[0].Group.Name].Items.Add(newListViewItem);
            recipesListView.Items.Add(newListViewItem);
            Content.Recipes.Add(newRecipe.id, newRecipe);
        }

        private void DuplicateSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            Deck newDeck = Content.Decks[decksListView.SelectedItems[0].Text].Copy();
            string id = newDeck.id;
            if (decksListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (decksListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newDeck.id = id;
            ListViewItem newListViewItem = new ListViewItem(newDeck.id) { Tag = newDeck.GetHashCode() };
            decksListView.Groups[decksListView.SelectedItems[0].Group.Name].Items.Add(newListViewItem);
            decksListView.Items.Add(newListViewItem);
            Content.Decks.Add(newDeck.id, newDeck);
        }

        private void DuplicateSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1) return;
            Legacy newLegacy = Content.Legacies[legaciesListView.SelectedItems[0].Text].Copy();
            string id = newLegacy.id;
            if (legaciesListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (legaciesListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newLegacy.id = id;
            ListViewItem newListViewItem = new ListViewItem(newLegacy.id) { Tag = newLegacy.GetHashCode() };
            legaciesListView.Groups[legaciesListView.SelectedItems[0].Group.Name].Items.Add(newListViewItem);
            legaciesListView.Items.Add(newListViewItem);
            Content.Legacies.Add(newLegacy.id, newLegacy);
        }

        private void DuplicateSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1) return;
            Ending newEnding = Content.Endings[endingsListView.SelectedItems[0].Text].Copy();
            string id = newEnding.id;
            if (endingsListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (endingsListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newEnding.id = id;
            ListViewItem newListViewItem = new ListViewItem(newEnding.id) { Tag = newEnding.GetHashCode() };
            endingsListView.Groups[endingsListView.SelectedItems[0].Group.Name].Items.Add(newListViewItem);
            endingsListView.Items.Add(newListViewItem);
            Content.Endings.Add(newEnding.id, newEnding);
        }

        private void DuplicateSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Verb newVerb = Content.Verbs[verbsListView.SelectedItems[0].Text].Copy();
            string id = newVerb.id;
            if (verbsListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (verbsListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newVerb.id = id;
            ListViewItem newListViewItem = new ListViewItem(newVerb.id) { Tag = newVerb.GetHashCode() };
            verbsListView.Groups[verbsListView.SelectedItems[0].Group.Name].Items.Add(newListViewItem);
            verbsListView.Items.Add(newListViewItem);
            Content.Verbs.Add(newVerb.id, newVerb);
        }

        private void ExportSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            Aspect exportedAspect = Content.GetAspect(aspectsListView.SelectedItems[0].Text);
            if (exportedAspect == null) return;
            ExportObject(exportedAspect, exportedAspect.id);
        }

        private void ExportSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            Element exportedElement = Content.GetElement(elementsListView.SelectedItems[0].Text);
            if (exportedElement == null) return;
            ExportObject(exportedElement, exportedElement.id);
        }

        private void ExportSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            Recipe exportedRecipe = Content.GetRecipe(recipesListView.SelectedItems[0].Text);
            if (exportedRecipe == null) return;
            ExportObject(exportedRecipe, exportedRecipe.id);
        }

        private void ExportSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            Deck exportedDeck = Content.GetDeck(decksListView.SelectedItems[0].Text);
            if (exportedDeck == null) return;
            ExportObject(exportedDeck, exportedDeck.id);
        }

        private void ExportSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1) return;
            Legacy exportedLegacy = Content.GetLegacy(legaciesListView.SelectedItems[0].Text);
            if (exportedLegacy == null) return;
            ExportObject(exportedLegacy, exportedLegacy.id);
        }

        private void ExportSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1) return;
            Ending exportedEnding = Content.GetEnding(endingsListView.SelectedItems[0].Text);
            if (exportedEnding == null) return;
            ExportObject(exportedEnding, exportedEnding.id);
        }

        private void ExportSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1) return;
            Verb exportedVerb = Content.GetVerb(verbsListView.SelectedItems[0].Text);
            ExportObject(exportedVerb, exportedVerb.id);
        }

        private void ExportObject(object objectToExport, string id)
        {
            string JSON = JsonConvert.SerializeObject(objectToExport, Formatting.Indented);
            saveFileDialog.FileName = objectToExport.GetType().Name + "_" + id + ".json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(saveFileDialog.OpenFile())))
                {
                    jtw.WriteRaw(JSON);
                }
            }
        }
        
        public void CopyObjectJSONToClipboard(object objectToExport)
        {
            Clipboard.SetText(Utilities.SerializeObject(objectToExport));
        }

        private void CopySelectedAspectJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            Aspect exportedAspect = Content.GetAspect(aspectsListView.SelectedItems[0].Text);
            if (exportedAspect == null) return;
            CopyObjectJSONToClipboard(exportedAspect);
        }

        private void CopySelectedElementJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            Element exportedElement = Content.GetElement(elementsListView.SelectedItems[0].Text);
            if (exportedElement == null) return;
            CopyObjectJSONToClipboard(exportedElement);
        }

        private void CopySelectedRecipeJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            Recipe exportedRecipe = Content.GetRecipe(recipesListView.SelectedItems[0].Text);
            if (exportedRecipe == null) return;
            CopyObjectJSONToClipboard(exportedRecipe);
        }

        private void CopySelectedDeckJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            Deck exportedDeck = Content.GetDeck(decksListView.SelectedItems[0].Text);
            if (exportedDeck == null) return;
            CopyObjectJSONToClipboard(exportedDeck);
        }

        private void CopySelectedLegacyJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1) return;
            Legacy exportedLegacy = Content.GetLegacy(legaciesListView.SelectedItems[0].Text);
            if (exportedLegacy == null) return;
            CopyObjectJSONToClipboard(exportedLegacy);
        }

        private void CopySelectedEndingJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1) return;
            Ending exportedEnding = Content.GetEnding(endingsListView.SelectedItems[0].Text);
            if (exportedEnding == null) return;
            CopyObjectJSONToClipboard(exportedEnding);
        }

        private void CopySelectedVerbJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1) return;
            Verb exportedVerb = Content.GetVerb(verbsListView.SelectedItems[0].Text);
            if (exportedVerb == null) return;
            CopyObjectJSONToClipboard(exportedVerb);
        }

        private void NewAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AspectViewer av = new AspectViewer(new Aspect(), AspectsList_Add);
            av.Show();
        }

        private void NewElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(new Element(), ElementsList_Add);
            ev.Show();
        }

        private void NewRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(new Recipe(), RecipesList_Add);
            rv.Show();
        }

        private void NewDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeckViewer dv = new DeckViewer(new Deck(), DecksList_Add);
            dv.Show();
        }

        private void NewLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LegacyViewer lv = new LegacyViewer(new Legacy(), LegaciesList_Add);
            lv.Show();
        }

        private void NewEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndingViewer ev = new EndingViewer(new Ending(), EndingsList_Add);
            ev.Show();
        }

        private void NewVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerbViewer vv = new VerbViewer(new Verb(), VerbsList_Add);
            vv.Show();
        }

        public void AspectsList_Add(object sender, Aspect result)
        {
            Content.Aspects[result.id] = result.Copy();
            ListViewGroup defaultAspectsGroup;
            if (aspectsListView.Groups["aspects"] == null)
            {
                defaultAspectsGroup = new ListViewGroup("aspects", "aspects");
                aspectsListView.Groups.Add(defaultAspectsGroup);
            }
            else
            {
                defaultAspectsGroup = aspectsListView.Groups["aspects"];
            }
            ListViewItem newAspectEntry = new ListViewItem(result.id) { Tag = result.GetHashCode() };
            defaultAspectsGroup.Items.Add(newAspectEntry);
            aspectsListView.Items.Add(newAspectEntry);
        }

        public void ElementsList_Add(object sender, Element result)
        {
            Content.Elements[result.id] = result.Copy();
            ListViewGroup defaultElementsGroup;
            if (elementsListView.Groups["elements"] == null)
            {
                defaultElementsGroup = new ListViewGroup("elements", "elements");
                elementsListView.Groups.Add(defaultElementsGroup);
            }
            else
            {
                defaultElementsGroup = elementsListView.Groups["elements"];
            }
            ListViewItem newElementEntry = new ListViewItem(result.id) { Tag = result.GetHashCode() };
            defaultElementsGroup.Items.Add(newElementEntry);
            elementsListView.Items.Add(newElementEntry);
        }

        public void RecipesList_Add(object sender, Recipe result)
        {
            Content.Recipes[result.id] = result.Copy();
            ListViewGroup defaultRecipesGroup;
            if (recipesListView.Groups["recipes"] == null)
            {
                defaultRecipesGroup = new ListViewGroup("recipes", "recipes");
                recipesListView.Groups.Add(defaultRecipesGroup);
            }
            else
            {
                defaultRecipesGroup = recipesListView.Groups["recipes"];
            }
            ListViewItem newRecipeEntry = new ListViewItem(result.id) { Tag = result.GetHashCode() };
            defaultRecipesGroup.Items.Add(newRecipeEntry);
            recipesListView.Items.Add(newRecipeEntry);
        }

        public void DecksList_Add(object sender, Deck result)
        {
            Content.Decks[result.id] = result.Copy();
            ListViewGroup defaultDecksGroup;
            if (decksListView.Groups["decks"] == null)
            {
                defaultDecksGroup = new ListViewGroup("decks", "decks");
                decksListView.Groups.Add(defaultDecksGroup);
            }
            else
            {
                defaultDecksGroup = decksListView.Groups["decks"];
            }
            ListViewItem newDeckEntry = new ListViewItem(result.id) { Tag = result.GetHashCode() };
            defaultDecksGroup.Items.Add(newDeckEntry);
            decksListView.Items.Add(newDeckEntry);
        }

        public void LegaciesList_Add(object sender, Legacy result)
        {
            Content.Legacies[result.id] = result.Copy();
            ListViewGroup defaultLegaciesGroup;
            if (legaciesListView.Groups["legacies"] == null)
            {
                defaultLegaciesGroup = new ListViewGroup("legacies", "legacies");
                legaciesListView.Groups.Add(defaultLegaciesGroup);
            }
            else
            {
                defaultLegaciesGroup = legaciesListView.Groups["legacies"];
            }
            ListViewItem newLegacyEntry = new ListViewItem(result.id) { Tag = result.GetHashCode() };
            defaultLegaciesGroup.Items.Add(newLegacyEntry);
            legaciesListView.Items.Add(newLegacyEntry);
        }

        public void EndingsList_Add(object sender, Ending result)
        {
            Content.Endings[result.id] = result.Copy();
            ListViewGroup defaultEndingsGroup;
            if (endingsListView.Groups["endings"] == null)
            {
                defaultEndingsGroup = new ListViewGroup("endings", "endings");
                endingsListView.Groups.Add(defaultEndingsGroup);
            }
            else
            {
                defaultEndingsGroup = endingsListView.Groups["endings"];
            }
            ListViewItem newEndingEntry = new ListViewItem(result.id) { Tag = result.GetHashCode() };
            defaultEndingsGroup.Items.Add(newEndingEntry);
            endingsListView.Items.Add(newEndingEntry);
        }

        public void VerbsList_Add(object sender, Verb result)
        {
            Content.Verbs[result.id] = result.Copy();
            ListViewGroup defaultVerbsGroup;
            if (verbsListView.Groups["verbs"] == null)
            {
                defaultVerbsGroup = new ListViewGroup("verbs", "verbs");
                verbsListView.Groups.Add(defaultVerbsGroup);
            }
            else
            {
                defaultVerbsGroup = verbsListView.Groups["verbs"];
            }
            ListViewItem newVerbEntry = new ListViewItem(result.id) { Tag = result.GetHashCode() };
            defaultVerbsGroup.Items.Add(newVerbEntry);
            verbsListView.Items.Add(newVerbEntry);
        }

        private void ModViewerTabControl_VisibleChanged(object sender, EventArgs e)
        {
            LoadWidths();
        }

        private void Splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter3_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter4_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter5_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter6_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter7_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void AspectsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = aspectsListView.SelectedItems[0].Text;
                if (editMode)
                {
                    AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), AspectsList_Assign);
                    av.Show();
                }
                else
                {
                    AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), null);
                    av.Show();
                }
            }
        }

        private void ElementsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = elementsListView.SelectedItems[0].Text;
                if (editMode)
                {
                    ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), ElementsList_Assign);
                    ev.Show();
                }
                else
                {
                    ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), null);
                    ev.Show();
                }
            }
        }

        private void RecipesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = recipesListView.SelectedItems[0].Text;
                if (editMode)
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), RecipesList_Assign);
                    rv.Show();
                }
                else
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), null);
                    rv.Show();
                }
            }
        }

        private void DecksListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = decksListView.SelectedItems[0].Text;
                if (editMode)
                {
                    DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), DecksList_Assign);
                    dv.Show();
                }
                else
                {
                    DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), null);
                    dv.Show();
                }
            }
        }

        private void LegaciesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = legaciesListView.SelectedItems[0].Text;
                if (editMode)
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), RecipesList_Assign);
                    rv.Show();
                }
                else
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), null);
                    rv.Show();
                }
            }
        }

        private void SetGroupAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            ListViewItem selectedItem = aspectsListView.SelectedItems[0];
            string currentGroup = mostRecentAspectsGroup;
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in aspectsListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (newGroup != currentGroup)
                {
                    if (currentGroup != "") aspectsListView.Groups[currentGroup].Items.Remove(selectedItem);
                    if (aspectsListView.Groups[newGroup] != null)
                    {
                        aspectsListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        aspectsListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                    }
                    mostRecentAspectsGroup = currentGroup;
                }
            }
            // string id = aspectsListView.SelectedItems[0].Text;
        }

        private void SetGroupElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            ListViewItem selectedItem = elementsListView.SelectedItems[0];
            string currentGroup = mostRecentElementsGroup;
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in elementsListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (newGroup != currentGroup)
                {
                    if (currentGroup != "") elementsListView.Groups[currentGroup].Items.Remove(selectedItem);
                    if (elementsListView.Groups[newGroup] != null)
                    {
                        elementsListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        elementsListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                    }
                    mostRecentElementsGroup = currentGroup;
                }
            }
        }

        private void SetGroupRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            ListViewItem selectedItem = recipesListView.SelectedItems[0];
            string currentGroup = mostRecentRecipesGroup;
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in recipesListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (newGroup != currentGroup)
                {
                    if (currentGroup != "") recipesListView.Groups[currentGroup].Items.Remove(selectedItem);
                    if (recipesListView.Groups[newGroup] != null)
                    {
                        recipesListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        recipesListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                    }
                    mostRecentRecipesGroup = currentGroup;
                }
            }
        }

        private void SetGroupDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            ListViewItem selectedItem = decksListView.SelectedItems[0];
            string currentGroup = mostRecentDecksGroup;
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in decksListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (newGroup != currentGroup)
                {
                    if (currentGroup != "") decksListView.Groups[currentGroup].Items.Remove(selectedItem);
                    if (decksListView.Groups[newGroup] != null)
                    {
                        decksListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        decksListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                    }
                    mostRecentDecksGroup = currentGroup;
                }
            }
        }

        private void SetGroupLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1) return;
            ListViewItem selectedItem = legaciesListView.SelectedItems[0];
            string currentGroup = mostRecentLegaciesGroup;
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in legaciesListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (newGroup != currentGroup)
                {
                    if (currentGroup != "") legaciesListView.Groups[currentGroup].Items.Remove(selectedItem);
                    if (legaciesListView.Groups[newGroup] != null)
                    {
                        legaciesListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        legaciesListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                    }
                    mostRecentLegaciesGroup = currentGroup;
                }
            }
        }

        private void SetGroupEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1) return;
            ListViewItem selectedItem = endingsListView.SelectedItems[0];
            string currentGroup = mostRecentEndingsGroup;
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in endingsListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (newGroup != currentGroup)
                {
                    if (currentGroup != "") endingsListView.Groups[currentGroup].Items.Remove(selectedItem);
                    if (endingsListView.Groups[newGroup] != null)
                    {
                        endingsListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        endingsListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                    }
                    mostRecentEndingsGroup = currentGroup;
                }
            }
        }

        private void SetGroupVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1) return;
            ListViewItem selectedItem = verbsListView.SelectedItems[0];
            string currentGroup = mostRecentVerbsGroup;
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in verbsListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (newGroup != currentGroup)
                {
                    if (currentGroup != "") verbsListView.Groups[currentGroup].Items.Remove(selectedItem);
                    if (verbsListView.Groups[newGroup] != null)
                    {
                        verbsListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        verbsListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                    }
                    mostRecentVerbsGroup = currentGroup;
                }
            }
        }

        private void EndingsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = endingsListView.SelectedItems[0].Text;
                if (editMode)
                {
                    EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), EndingsList_Assign);
                    ev.Show();
                }
                else
                {
                    EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), null);
                    ev.Show();
                }
            }
        }

        private void VerbsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = verbsListView.SelectedItems[0].Text;
                if (editMode)
                {
                    VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), VerbsList_Assign);
                    vv.Show();
                }
                else
                {
                    VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), null);
                    vv.Show();
                }
            }
        }


    }
}
