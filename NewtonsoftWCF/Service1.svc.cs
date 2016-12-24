using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewtonsoftWCF
{
    public class Service1 : IService1
    {
        static string cJson = @"{
	MyStuff : [{
			ConfigurationType : 1,
			Configuration : {
				Wheels : 2
			}
		}, {
			ConfigurationType : 2,
			Configuration : {
				Legs : 4
			}
		}
	]
}";

        public Stuff GetData()
        {
            var resolver = new ThingContractResolver();
            var result= JsonConvert.DeserializeObject<Stuff>(cJson, new JsonSerializerSettings
            {
                ContractResolver = resolver
            });
            return result;
        }

        public void SetData(Stuff stuff)
        {
            var resolver = new ThingContractResolver();
            cJson = JsonConvert.SerializeObject(stuff, new JsonSerializerSettings
            {
                ContractResolver = resolver
            });
        }
    }
}
