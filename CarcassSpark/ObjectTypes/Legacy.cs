﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Legacy : Interfaces.ICultSimGameObject
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string startdescription, image, fromEnding, startingVerbId, tablecoverimage, tablesurfaceimage, tableedgeimage;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? availableWithoutEndingMatch, newstart;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> excludesOnEnding;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "excludesOnEnding$append")]
        public List<string> excludesOnEnding_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "excludesOnEnding$prepend")]
        public List<string> excludesOnEnding_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "excludesOnEnding$remove")]
        public List<string> excludesOnEnding_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> effects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$add")]
        public Dictionary<string, int> effects_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$remove")]
        public List<string> effects_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> statusbarelements;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statusbarelements&prepend")]
        public List<string> statusbarelements_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statusbarelements&append")]
        public List<string> statusbarelements_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statusbarelements&remove")]
        public List<string> statusbarelements_remove;

        [JsonConstructor]
        public Legacy(string id, string label, string description, string startdescription, string comments, string tablecoverimage, string tablesurfaceimage, string tableedgeimage,
                      Dictionary<string, int> effects, string image, string fromEnding, bool? availableWithoutEndingMatch, bool? newstart,
                      string startingVerbId, List<string> excludesOnEnding,
                      List<string> excludesOnEnding_prepend, List<string> excludesOnEnding_append, List<string> excludesOnEnding_remove,
                      Dictionary<string, int> effects_extend, List<string> effects_remove, List<string> statusbarelements,
                      List<string> statusbarelements_prepend, List<string> statusbarelements_append, List<string> statusbarelements_remove,
                      bool? deleted, List<string> extends)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.startdescription = startdescription;
            this.effects = effects;
            this.effects_extend = effects_extend;
            this.effects_remove = effects_remove;
            this.comments = comments;
            this.tablecoverimage = tablecoverimage;
            this.tableedgeimage = tableedgeimage;
            this.tablesurfaceimage = tablesurfaceimage;
            this.image = image;
            this.fromEnding = fromEnding;
            this.availableWithoutEndingMatch = availableWithoutEndingMatch;
            this.startingVerbId = startingVerbId;
            this.excludesOnEnding = excludesOnEnding;
            this.excludesOnEnding_prepend = excludesOnEnding_prepend;
            this.excludesOnEnding_append = excludesOnEnding_append;
            this.excludesOnEnding_remove = excludesOnEnding_remove;
            this.statusbarelements = statusbarelements;
            this.statusbarelements_prepend = statusbarelements_prepend;
            this.statusbarelements_append = statusbarelements_append;
            this.statusbarelements_remove = statusbarelements_remove;
            this.deleted = deleted;
            this.newstart = newstart;
            this.extends = extends;
        }

        public Legacy()
        {

        }

        public new Legacy Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Legacy>(serializedObject);
        }

        public override string GetTypeUppercaseName()
        {
            return "Legacy";
        }

        public override string GetTypeDisplayName()
        {
            return "Legacies";
        }

        public override string GetTypeJSONName()
        {
            return "legacies";
        }
    }
}
