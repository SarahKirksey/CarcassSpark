﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Manifest
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string author;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string version;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description_long;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Dependency> dependencies;

        public Manifest(string name, string author, string version, string description, string description_long)
        {
            this.name = name;
            this.author = author;
            this.version = version;
            this.description = description;
            this.description_long = description_long;
        }

        public Manifest(string name, string author, string version, string description, string description_long, List<Dependency> dependencies)
        {
            this.name = name;
            this.author = author;
            this.version = version;
            this.description = description;
            this.description_long = description_long;
            this.dependencies = dependencies;
        }


        public Manifest()
        {
            name = "";
            author = "";
            version = "";
            description = "";
            description_long = "";
        }

        public class Dependency
        {
            public string modId;
            public string version;
            public string VersionOperator;

            public Dependency(string modId, string version, string VersionOperator)
            {
                this.modId = modId;
                this.version = version;
                this.VersionOperator = VersionOperator;
            }

            public Dependency()
            {

            }
        }
    }
}
