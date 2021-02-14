using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Verb : Interfaces.ICultSimGameObject
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Slot slot;

        [JsonConstructor]
        public Verb(string id, string label, string description, string comments, bool? deleted, Slot slot, List<string> extends)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.comments = comments;
            this.slot = slot;
            this.deleted = deleted;
            this.extends = extends;
        }

        public Verb()
        {

        }

        public new Verb Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Verb>(serializedObject);
        }
    }
}
