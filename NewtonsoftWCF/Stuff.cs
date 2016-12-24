using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NewtonsoftWCF
{
    public class Stuff
    {
        public List<Thing> MyStuff;
    }

    [KnownType(typeof(Vehicle))]
    [KnownType(typeof(Animal))]
    public class Configuration
    {
    }

    public class Vehicle : Configuration
    {
        public int Wheels;
    }

    public class Animal : Configuration
    {
        public int Legs;
    }

    [DataContract]
    public class Thing
    {
        private Configuration _configuration;
        private JToken _jtoken;

        [DataMember(Name = "ConfigurationType")]
        public int ConfigurationType;

        [IgnoreDataMember]
        public JToken Configuration
        {
            get
            {
                return _jtoken;
            }
            set
            {
                _jtoken = value;
                cleanWCFTypeFrom(_jtoken);
                _configuration = null;
            }
        }

        [JsonIgnore]
        [DataMember(Name = "Configuration")]
        public Configuration ConfigurationObject
        {
            get
            {
                if (_configuration == null)
                {
                    switch (ConfigurationType)
                    {
                        case 1: _configuration = Configuration.ToObject<Vehicle>(); break;
                        case 2: _configuration = Configuration.ToObject<Animal>(); break;
                    }
                }
                return _configuration;
            }
            set
            {
                _configuration = value;
                _jtoken = JRaw.FromObject(value);
            }
        }
    }

    public class ThingContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member,
               Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (member.Name == "Configuration") prop.Ignored = false;
            return prop;
        }
    }
}