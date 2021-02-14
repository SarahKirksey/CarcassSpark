using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Ending : Interfaces.ICultSimGameObject
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string image, flavour, anim, achievement;

        [JsonConstructor]
        public Ending(string id, string label, string description, string image, string flavour, string anim, string achievement, string comments, bool? deleted, List<string> extends)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.image = image;
            this.flavour = flavour;
            this.anim = anim;
            this.achievement = achievement;
            this.comments = comments;
            this.deleted = deleted;
            this.extends = extends;
        }

        public Ending()
        {

        }

        public new Ending Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Ending>(serializedObject);
        }

        public override string GetTypeUppercaseName()
        {
            return "Ending";
        }

        public override string GetTypeDisplayName()
        {
            return "Endings";
        }

        public override string GetTypeJSONName()
        {
            return "endings";
        }
    }
}
