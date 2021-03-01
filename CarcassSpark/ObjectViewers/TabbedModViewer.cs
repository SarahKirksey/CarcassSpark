﻿using CarcassSpark.ObjectTypes;
using CarcassSpark.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class TabbedModViewer : Form
    {
        private ModViewerTabControl SelectedModViewer;

        public TabbedModViewer()
        {
            // this should always be first before messing with any components like the Folder Browser Dialog
            InitializeComponent();
            // This is necessary to ensure we have a reference point for the game and the game's assemblies

        }

        private void InitializeTabs()
        {
            CreateNewModViewerTab(Utilities.DirectoryToVanillaContent, true, false);
            if (Settings.settings["previousMods"] != null && Settings.settings["loadPreviousMods"] != null && Settings.settings["loadPreviousMods"].ToObject<bool>())
            {
                if (((JArray)Settings.settings["previousMods"]).Count() > 0)
                {
                    foreach (string path in Settings.GetPreviousMods())
                    {
                        CreateNewModViewerTab(path, false, false);
                    }
                }
            }
            if (!SelectedModViewer.isVanilla)
            {

            }
            toggleEditModeToolStripMenuItem.Checked = !SelectedModViewer.isVanilla && SelectedModViewer.editMode;
            saveSplitterLocationsToolStripMenuItem.Checked = !SelectedModViewer.isVanilla && (SelectedModViewer.Content.GetCustomManifestBool("saveWidths") ?? false);
        }
        private void CreateNewModViewerTab(string folder, bool isVanilla, bool newMod)
        {
            try
            {
                SelectedModViewer = new ModViewerTabControl(folder, isVanilla, newMod);
                SelectedModViewer.MarkDirtyEventHandler += MarkTabDirty;
                TabPage newPage = new TabPage(SelectedModViewer.Content.GetName());
                newPage.Controls.Add(SelectedModViewer);
                newPage.Name = SelectedModViewer.Content.GetName();
                ModViewerTabs.TabPages.Add(newPage);
                ModViewerTabs.SelectTab(newPage);
            }
            catch
            {

            }
        }

        private void CreateNewModViewerTab(ModViewerTabControl mvtc)
        {
            SelectedModViewer = mvtc;
            TabPage newPage = new TabPage(mvtc.Content.GetName());
            newPage.Controls.Add(mvtc);
            newPage.Name = mvtc.Content.GetName();
            ModViewerTabs.TabPages.Add(newPage);
            ModViewerTabs.SelectTab(newPage);
        }

        private void OpenModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = (Settings.settings["previousMods"] != null && Settings.settings["previousMods"].Count() > 0) ? Settings.settings["previousMods"].Last.ToString() : AppDomain.CurrentDomain.BaseDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string location = folderBrowserDialog.SelectedPath;
                try
                {
                    ModViewerTabControl mvtc = new ModViewerTabControl(location, false, false);
                    mvtc.MarkDirtyEventHandler += MarkTabDirty;
                    CreateNewModViewerTab(mvtc);
                    if (Settings.HasPreviousMods())
                    {
                        Settings.AddPreviousMod(location);
                    }
                    else
                    {
                        Settings.InitPreviousMods(location);
                    }
                    Settings.SaveSettings();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Opening Mod");
                    return;
                }
            }
        }

        private void NewModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string location = folderBrowserDialog.SelectedPath;
                try
                {
                    ModViewerTabControl mvtc = new ModViewerTabControl(location, false, true);
                    mvtc.MarkDirtyEventHandler += MarkTabDirty;
                    CreateNewModViewerTab(mvtc);
                    if (Settings.HasPreviousMods())
                    {
                        Settings.AddPreviousMod(location);
                    }
                    else
                    {
                        Settings.InitPreviousMods(location);
                    }
                    Settings.SaveSettings();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Creating Mod");
                }
            }
        }

        private void CloseModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Carcass Spark will not close Vanilla content.", "Close Mod", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (SelectedModViewer.IsDirty && SelectedModViewer.editMode)
            {
                if (MessageBox.Show("You WILL lose any unsaved changes you've made. Click OK to discard changes and close the mod.",
                    "You have unsaved changes",
                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }
            Utilities.ContentSources.Remove(SelectedModViewer.Content.GetName());
            // ((JArray)Settings.settings["previousMods"]).Remove(SelectedModViewer.Content.currentDirectory);
            Settings.RemovePreviousMod(SelectedModViewer.Content.currentDirectory);
            ModViewerTabs.TabPages.Remove(ModViewerTabs.SelectedTab);
        }

        private void SettingsToolStripButton_Click(object sender, EventArgs e)
        {
            new Settings().Show();
        }

        private void SaveModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Can't save vanilla content.");
                return;
            }
            SelectedModViewer.SaveMod();
        }

        private void ModViewerTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModViewerTabs.SelectedTab == null)
            {
                SelectedModViewer = null;
                return;
            }
            SelectedModViewer = (ModViewerTabControl)ModViewerTabs.SelectedTab.Controls[0];

            toggleEditModeToolStripMenuItem.Checked = !SelectedModViewer.isVanilla && SelectedModViewer.editMode;
            toggleAutosaveToolStripMenuItem.Checked = !SelectedModViewer.isVanilla && (SelectedModViewer.Content.GetCustomManifestBool("AutoSave") ?? false);
            saveSplitterLocationsToolStripMenuItem.Checked = SelectedModViewer.isVanilla ? (Settings.settings.ContainsKey("saveWidths") && Settings.settings["saveWidths"].ToObject<bool>()) : (SelectedModViewer.Content.GetCustomManifestBool("saveWidths") ?? false);

            toggleEditModeToolStripMenuItem.Enabled = !SelectedModViewer.isVanilla;
            toggleAutosaveToolStripMenuItem.Enabled = !SelectedModViewer.isVanilla;
        }

        private void SaveModToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = SelectedModViewer.Content.currentDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedModViewer.SaveMod(folderBrowserDialog.SelectedPath);
            }
        }

        private void OpenSynopsisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("There is no synopsis for vanilla content.");
                return;
            }

            SynopsisViewer mv = new SynopsisViewer(SelectedModViewer.Content.synopsis.Copy());
            if (mv.ShowDialog() == DialogResult.OK)
            {
                if (SelectedModViewer.Content.GetName() != mv.displayedSynopsis.name)
                {
                    SelectedModViewer.Parent.Name = mv.displayedSynopsis.name;
                }
                SelectedModViewer.Content.synopsis = mv.displayedSynopsis.Copy();
                SelectedModViewer.SaveMod();
            }
        }

        private void ReloadContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.LoadContent();
        }

        private void ToggleEditModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Can't enable Edit Mode for vanilla content.");
                return;
            }
            toggleEditModeToolStripMenuItem.Checked = !toggleEditModeToolStripMenuItem.Checked;
            SelectedModViewer.Content.SetEditMode(toggleEditModeToolStripMenuItem.Checked);
            SelectedModViewer.SetEditingMode(toggleEditModeToolStripMenuItem.Checked);
        }

        private void ToggleAutosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Can't enable Autosave for vanilla content.");
                return;
            }
            toggleAutosaveToolStripMenuItem.Checked = !toggleAutosaveToolStripMenuItem.Checked;
            SelectedModViewer.Content.SetCustomManifestProperty("AutoSave", toggleAutosaveToolStripMenuItem.Checked);
        }

        #region "Create New" events

        private void AspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Aspect>(SelectedModViewer.AspectsList_Add);
        }

        private void ElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Element>(SelectedModViewer.ElementsList_Add);
        }

        private void RecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Recipe>(SelectedModViewer.RecipesList_Add);
        }

        private void DeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Deck>(SelectedModViewer.DecksList_Add);
        }

        private void LegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Legacy>(SelectedModViewer.LegaciesList_Add);
        }

        private void EndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Ending>(SelectedModViewer.EndingsList_Add);
        }

        private void VerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Verb>(SelectedModViewer.VerbsList_Add);
        }

        private void CreateNewGameObject<T>(EventHandler<T> successCallback) where T: IGameObject, new()
        {
            if(!SelectedModViewer.isVanilla)
            {
                IGameObjectViewer<T> gov = Utilities.GetViewer<T>(new T(), (EventHandler<T>)successCallback);
                gov.Show();
            }
        }

        #endregion
        #region "Import" events

        private void AspectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(SelectedModViewer.Content.Aspects);
        }

        private void ElementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(SelectedModViewer.Content.Elements);
        }

        private void RecipeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(SelectedModViewer.Content.Recipes);
        }

        private void DeckToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(SelectedModViewer.Content.Decks);
        }

        private void LegacyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(SelectedModViewer.Content.Legacies);
        }

        private void EndingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(SelectedModViewer.Content.Endings);
        }

        private void VerbToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(SelectedModViewer.Content.Verbs);
        }

        private void ImportGameObject<T>(ContentGroup<T> cg) where T : IGameObject
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    T deserializedT = JsonConvert.DeserializeObject<T>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    ListView listView = SelectedModViewer.ListViews[cg.DisplayName];
                    if (listView.Items.ContainsKey(deserializedT.ID))
                    {
                        MessageBox.Show(cg.DisplayName + " already exists, overwriting.");
                    }
                    else
                    {
                        ListViewGroup group = listView.Groups[cg.Filename] ?? new ListViewGroup(cg.Filename, cg.Filename);
                        if (!listView.Groups.Contains(group))
                        {
                            listView.Groups.Add(group);
                        }
                        listView.Items.Add(new ListViewItem(deserializedT.ID) { Tag = guid, Group = group });
                    }
                    deserializedT.Filename = cg.Filename;
                    cg[guid] = deserializedT;
                    SelectedModViewer.MarkDirty();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing " + cg.DisplayName);
                }
            }
        }

        #endregion

        private void FromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (!Clipboard.ContainsText())
            {
                return;
            }

            JsonEditor je = new JsonEditor(Clipboard.GetText());
            if (je.ShowDialog() == DialogResult.OK)
            {
                switch (je.objectType)
                {
                    case "Aspect":
                        FromClipboardOnOkay(SelectedModViewer.Content.Aspects, je.objectText);
                        break;
                    case "Element":
                        FromClipboardOnOkay(SelectedModViewer.Content.Elements, je.objectText);
                        break;
                    case "Recipe":
                        FromClipboardOnOkay(SelectedModViewer.Content.Recipes, je.objectText);
                        break;
                    case "Deck":
                        FromClipboardOnOkay(SelectedModViewer.Content.Decks, je.objectText);
                        break;
                    case "Legacy":
                        FromClipboardOnOkay(SelectedModViewer.Content.Legacies, je.objectText);
                        break;
                    case "Ending":
                        FromClipboardOnOkay(SelectedModViewer.Content.Endings, je.objectText);
                        break;
                    case "Verb":
                        FromClipboardOnOkay(SelectedModViewer.Content.Verbs, je.objectText);
                        break;
                    default:
                        MessageBox.Show("I'm not sure what you selected or how, but that was an invalid choice.", "Unknown Object Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                }
                SelectedModViewer.MarkDirty();
            }
        }

        private void FromClipboardOnOkay<T>(ContentGroup<T> cg, string objectText) where T : IGameObject
        {
            Guid guid = Guid.NewGuid();
            ListViewGroup listViewGroup = SelectedModViewer.ListViews[cg.Filename].Groups[cg.Filename] ?? new ListViewGroup(cg.Filename, cg.Filename);
            if (SelectedModViewer.ListViews[cg.Filename].Groups.Contains(listViewGroup))
            {
                SelectedModViewer.ListViews[cg.Filename].Groups.Add(listViewGroup);
            }
            T deserializedGameObject = JsonConvert.DeserializeObject<T>(objectText);
            deserializedGameObject.Filename = cg.Filename;
            cg[guid] = deserializedGameObject;
            if (!SelectedModViewer.ListViews[cg.Filename].Items.ContainsKey(deserializedGameObject.ID))
            {
                ListViewItem item = new ListViewItem(deserializedGameObject.ID) { Tag = guid, Group = listViewGroup };
                SelectedModViewer.ListViews[cg.Filename].Items.Add(item);
                // listViewGroup.Items.Add(item);
            }
        }

        private void SummonGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            SummonCreator sc = new SummonCreator();
            if (sc.ShowDialog() == DialogResult.OK)
            {
                ListViewGroup defaultElementsGroup = SelectedModViewer.elementsListView.Groups["elements"] ?? new ListViewGroup("elements", "elements");
                if (!SelectedModViewer.elementsListView.Groups.Contains(defaultElementsGroup))
                {
                    SelectedModViewer.elementsListView.Groups.Add(defaultElementsGroup);
                }

                Guid baseGuid = Guid.NewGuid();
                ListViewItem baseSummon = new ListViewItem(sc.baseSummon.id) { Tag = baseGuid, Group = defaultElementsGroup };
                SelectedModViewer.elementsListView.Items.Add(baseSummon);
                sc.baseSummon.filename = "elements";
                SelectedModViewer.Content.Elements.Add(baseGuid, sc.baseSummon.Copy());

                Guid preGuid = Guid.NewGuid();
                ListViewItem preSummon = new ListViewItem(sc.preSummon.id) { Tag = preGuid, Group = defaultElementsGroup };
                SelectedModViewer.elementsListView.Items.Add(preSummon);
                sc.preSummon.filename = "elements";
                SelectedModViewer.Content.Elements.Add(preGuid, sc.preSummon.Copy());

                ListViewGroup defaultRecipesGroup = SelectedModViewer.elementsListView.Groups["recipes"] ?? new ListViewGroup("recipes", "recipes");
                if (!SelectedModViewer.elementsListView.Groups.Contains(defaultElementsGroup))
                {
                    SelectedModViewer.elementsListView.Groups.Add(defaultElementsGroup);
                }

                Guid startGuid = Guid.NewGuid();
                ListViewItem startSummon = new ListViewItem(sc.startSummon.id) { Tag = startGuid, Group = defaultRecipesGroup };
                SelectedModViewer.recipesListView.Items.Add(startSummon);
                sc.startSummon.filename = "recipes";
                SelectedModViewer.Content.Recipes.Add(startGuid, sc.startSummon.Copy());

                Guid succeedGuid = Guid.NewGuid();
                ListViewItem succeedSummon = new ListViewItem(sc.succeedSummon.id) { Tag = succeedGuid, Group = defaultRecipesGroup };
                SelectedModViewer.recipesListView.Items.Add(succeedSummon);
                sc.succeedSummon.filename = "recipes";
                SelectedModViewer.Content.Recipes.Add(succeedGuid, sc.succeedSummon.Copy());
            }
        }

        private void ImageImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            ImageImporter ii = new ImageImporter();
            if (ii.ShowDialog() == DialogResult.OK)
            {
                switch (ii.displayedImageType.ToLower())
                {
                    case "aspect":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\aspects\\" + ii.displayedFileName, true);
                        break;
                    case "element":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\elements\\" + ii.displayedFileName, true);
                        break;
                    case "ending":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\endings\\" + ii.displayedFileName, true);
                        break;
                    case "legacy":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\legacies\\" + ii.displayedFileName, true);
                        break;
                    case "verb":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\verbs\\" + ii.displayedFileName, true);
                        break;
                }
                MessageBox.Show("Imported " + ii.displayedImageType + " image.");
            }
        }

        private void JSONCleanerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JsonCleaner jc = new JsonCleaner();
            jc.ShowDialog();
        }

        private void SaveSplitterLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool @checked = saveSplitterLocationsToolStripMenuItem.Checked;
            saveSplitterLocationsToolStripMenuItem.Checked = !@checked;
            if (!@checked)
            {
                if (!SelectedModViewer.isVanilla)
                {
                    SelectedModViewer.Content.SetCustomManifestProperty("saveWidths", !@checked);
                    SelectedModViewer.Content.SetCustomManifestProperty("widths", new List<int>() {
                        SelectedModViewer.tableLayoutPanel2.Size.Width,
                        SelectedModViewer.tableLayoutPanel3.Size.Width,
                        SelectedModViewer.tableLayoutPanel4.Size.Width,
                        SelectedModViewer.tableLayoutPanel5.Size.Width,
                        SelectedModViewer.tableLayoutPanel6.Size.Width,
                        SelectedModViewer.tableLayoutPanel7.Size.Width,
                        SelectedModViewer.tableLayoutPanel8.Size.Width,
                    });
                }
                else
                {
                    Settings.settings["saveWidths"] = !@checked;
                    Settings.settings["widths"] = JToken.FromObject(new List<int>() {
                        SelectedModViewer.tableLayoutPanel2.Size.Width,
                        SelectedModViewer.tableLayoutPanel3.Size.Width,
                        SelectedModViewer.tableLayoutPanel4.Size.Width,
                        SelectedModViewer.tableLayoutPanel5.Size.Width,
                        SelectedModViewer.tableLayoutPanel6.Size.Width,
                        SelectedModViewer.tableLayoutPanel7.Size.Width,
                        SelectedModViewer.tableLayoutPanel8.Size.Width,
                    });
                    Settings.SaveSettings();
                }

            }
            else
            {
                if (!SelectedModViewer.isVanilla)
                {
                    SelectedModViewer.Content.GetCustomManifest().Remove("saveWidths");
                }
                else
                {
                    Settings.settings.Remove("saveWidths");
                    Settings.SaveSettings();
                }
            }
            SelectedModViewer.SaveManifests(SelectedModViewer.Content.currentDirectory);
        }

        private void CulturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CulturesViewer cv = new CulturesViewer(SelectedModViewer.Content.Cultures.GameObjects, SelectedModViewer.editMode);
            if (cv.ShowDialog() == DialogResult.OK)
            {
                SelectedModViewer.Content.Cultures.GameObjects = cv.displayedCultures.ToDictionary(entry => Guid.NewGuid(), entry => entry.Value.Copy());
            }
        }

        private void AssetBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssetBrowser ab = new AssetBrowser();
            ab.Show();
        }

        private void TemplateManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager();
            templateManager.Show();
        }

        private void AboutToolStripButton_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void ModViewerTabs_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ModViewerTabs_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string path in files)
                {
                    if (Path.HasExtension(path))
                    {   // We are only interested in paths, if there's an extension then it's a file so we'll skip it
                        continue;
                    }
                    // Then we check for a synopsis or a manifest to see if we're actually looking at a valid mod folder
                    string synopsisPath = Path.Combine(path, "synopsis.json");
                    string manifestPath = Path.Combine(path, "manifest.json");
                    if (File.Exists(synopsisPath) || File.Exists(manifestPath))
                    {
                        if (Settings.HasPreviousMods())
                        {
                            Settings.AddPreviousMod(path);
                        }
                        else
                        {
                            Settings.InitPreviousMods(path);
                        }
                        CreateNewModViewerTab(path, false, false);
                    }
                    else
                    {
                        MessageBox.Show("There is not a 'synopsis.json' file in this folder.");
                    }
                }
            }
        }

        private void OpenInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "Explorer.exe",
                Arguments = SelectedModViewer.Content.currentDirectory
            };
            Process.Start(startInfo);
        }

        public void MarkTabDirty(object modViewerTab, bool v)
        {
            TabPage tabPage = (TabPage)((ModViewerTabControl)modViewerTab).Parent;
            tabPage.Text = v ? tabPage.Name + "*" : tabPage.Name;
        }

        private void ModViewerTabs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                // Thanks, Samuel from StackOverflow for this 12 year old solution
                TabPage tab = ModViewerTabs.TabPages.Cast<TabPage>().Where((t, i) => ModViewerTabs.GetTabRect(i).Contains(e.Location)).First();
                // Selecting a tab fires an event handler that'll update SelectedModViewer, so we can just use that variable
                ModViewerTabs.SelectTab(tab);

                // TODO deduplicate the code below
                if (SelectedModViewer.isVanilla)
                {
                    MessageBox.Show("Carcass Spark will not close Vanilla content.", "Close Mod", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (SelectedModViewer.IsDirty && SelectedModViewer.editMode)
                {
                    if (MessageBox.Show("You WILL lose any unsaved changes you've made. Click OK to discard changes and close the mod.",
                        "You have unsaved changes",
                        MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                }
                Utilities.ContentSources.Remove(SelectedModViewer.Content.GetName());
                Settings.RemovePreviousMod(SelectedModViewer.Content.currentDirectory);
                SelectedModViewer.Dispose();
                ModViewerTabs.TabPages.Remove(tab);
            }
        }

        #region "Unhide Groups" events

        private void AspectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups(SelectedModViewer.Content.Aspects.Filename);
            SelectedModViewer.SaveCustomManifest(SelectedModViewer.Content.currentDirectory);
            SelectedModViewer.ReloadListView(SelectedModViewer.Content.Aspects);
        }

        private void ElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups(SelectedModViewer.Content.Elements.Filename);
            SelectedModViewer.SaveCustomManifest(SelectedModViewer.Content.currentDirectory);
            SelectedModViewer.ReloadListView(SelectedModViewer.Content.Elements);
        }

        private void RecipesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups(SelectedModViewer.Content.Recipes.Filename);
            SelectedModViewer.SaveCustomManifest(SelectedModViewer.Content.currentDirectory);
            SelectedModViewer.ReloadListView(SelectedModViewer.Content.Recipes);
        }

        private void DecksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups(SelectedModViewer.Content.Decks.Filename);
            SelectedModViewer.SaveCustomManifest(SelectedModViewer.Content.currentDirectory);
            SelectedModViewer.ReloadListView(SelectedModViewer.Content.Decks);
        }

        private void LegaciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups(SelectedModViewer.Content.Legacies.Filename);
            SelectedModViewer.SaveCustomManifest(SelectedModViewer.Content.currentDirectory);
            SelectedModViewer.ReloadListView(SelectedModViewer.Content.Legacies);
        }

        private void EndingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups(SelectedModViewer.Content.Endings.Filename);
            SelectedModViewer.SaveCustomManifest(SelectedModViewer.Content.currentDirectory);
            SelectedModViewer.ReloadListView(SelectedModViewer.Content.Endings);
        }

        private void VerbsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups(SelectedModViewer.Content.Verbs.Filename);
            SelectedModViewer.SaveCustomManifest(SelectedModViewer.Content.currentDirectory);
            SelectedModViewer.ReloadListView(SelectedModViewer.Content.Verbs);
        }

        #endregion

        private void TabbedModViewer_Shown(object sender, EventArgs e)
        {
            if (Settings.settings["GamePath"] == null)
            {
                // If installed in the game's folder, save the user the hassle and just use that install
                if (File.Exists("cultistsimulator.exe"))
                {
                    Settings.settings["GamePath"] = AppDomain.CurrentDomain.BaseDirectory;
                    Settings.settings["portable"] = false;
                }
                else
                {
                    // Otherwise, make them select the game's installation folder
                    MessageBox.Show("Please select your Cultist Simulator game directory.");
                    folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                    if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    {
                        // Didn't open the games folder
                        MessageBox.Show("No directory selected, exiting.");
                        Application.Exit();
                        return;
                    }

                    // Check to see if the game's actually installed there
                    if (!File.Exists(folderBrowserDialog.SelectedPath + "/cultistsimulator.exe"))
                    {
                        MessageBox.Show("cultistsimulator.exe not found in that folder, please select your install folder.", "Wrong Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }
                    
                    Settings.settings["portable"] = true;
                    Settings.settings["GamePath"] = folderBrowserDialog.SelectedPath;
                }
                Settings.SaveSettings();
            }

            InitializeTabs();
        }

        private void hiddenGroupManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HiddenGroupManager hiddenGroupManager = new HiddenGroupManager(SelectedModViewer.Content.GetHiddenGroupsDictionary());
            if (hiddenGroupManager.ShowDialog() == DialogResult.OK)
            {
                SelectedModViewer.Content.SetHiddenGroups(hiddenGroupManager.HiddenGroups);
                SelectedModViewer.SaveCustomManifest();
                SelectedModViewer.LoadContent();
            }
        }
    }
}