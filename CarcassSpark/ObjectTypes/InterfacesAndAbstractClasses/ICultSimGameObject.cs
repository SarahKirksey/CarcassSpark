using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarcassSpark.ObjectTypes.Interfaces
{
    public abstract class ICultSimGameObject
    {
        [JsonIgnore]
        public string filename;
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, comments;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? deleted;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        public abstract string GetTypeUppercaseName();  // format: Aspect, Element, Recipe, etc.
        public abstract string GetTypeSingularName();   // format: aspect, element, recipe, etc.
        public abstract string GetTypeDisplayName();    // format: Aspects, Elements, Recipes, etc.
        public abstract string GetTypeName();           // format: aspects, elements, recipes, etc.
        public abstract string GetTypeJSONName();       // format: elements, elements, recipes, etc.
        
        

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public ICultSimGameObject Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<ICultSimGameObject>(serializedObject);
        }
    }
}
