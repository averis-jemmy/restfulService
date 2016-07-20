using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestfulTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IResftfulService" in both code and config file together.
    [ServiceContract]
    public interface IRestfulService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "loginJson")]
        UserDetails LoginJsonData(RequestLogin data);
    }

    [DataContract(Namespace = "http://www.contoso.com")]
    public class RequestLogin
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string Password { get; set; }
    }

    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserToken { get; set; }
    }
}
