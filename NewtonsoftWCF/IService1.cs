using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewtonsoftWCF
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "/getData")]
        Stuff GetData();

        [OperationContract]
        [WebInvoke(UriTemplate = "/setData", Method = "POST")]
        void SetData(Stuff stuff);
    }
}
