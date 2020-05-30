﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Deck
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, comments, defaultcard;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> spec; // the actual internal deck
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "spec$append")]
        public List<string> spec_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "spec$prepend")]
        public List<string> spec_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "spec$remove")]
        public List<string> spec_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? resetonexhaustion;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? draws, defaultdraws;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "draws$add")]
        public int? draws_add;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "draws$minus")]
        public int? draws_minus;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "defaultdraws$add")]
        public int? defaultdraws_add;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "defaultdraws$minus")]
        public int? defaultdraws_minus;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> drawmessages, defaultdrawmessages;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "drawmessages$extend")]
        public Dictionary<string, string> drawmessages_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "drawmessages$remove")]
        public List<string> drawmessages_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "defaultdrawmessages$extend")]
        public Dictionary<string, string> defaultdrawmessages_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "defaultdrawmessages$remove")]
        public List<string> defaultdrawmessages_remove;

        [JsonConstructor]
        public Deck(List<string> spec, int? defaultdraws, int? defaultdraws_add, int? defaultdraws_minus, int? draws, int? draws_add, int? draws_minus, bool? resetonexhaustion, string id, string label, string description, string comments,
                    string defaultcard, Dictionary<string, string> drawmessages,
                    Dictionary<string, string> defaultdrawmessages, List<string> spec_append, List<string> spec_prepend, List<string> spec_remove,
                    Dictionary<string, string> drawmessages_extend, List<String> drawmessages_remove, Dictionary<string, string> defaultdrawmessages_extend, List<String> defaultdrawmessages_remove)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.spec = spec;
            this.spec_append = spec_append;
            this.spec_prepend = spec_prepend;
            this.spec_remove = spec_remove;
            this.comments = comments;
            this.defaultcard = defaultcard;
            this.resetonexhaustion = resetonexhaustion;
            this.defaultdraws = defaultdraws;
            this.defaultdraws_add = defaultdraws_add;
            this.defaultdraws_minus = defaultdraws_minus;
            this.draws = draws;
            this.draws_add = draws_add;
            this.draws_minus = draws_minus;
            this.drawmessages = drawmessages;
            this.drawmessages_extend = drawmessages_extend;
            this.drawmessages_remove = drawmessages_remove;
            this.defaultdrawmessages = defaultdrawmessages;
            this.defaultdrawmessages_extend = defaultdrawmessages_extend;
            this.defaultdrawmessages_remove = defaultdrawmessages_remove;
        }
        
        public Deck()
        {

        }
        
    }

}
