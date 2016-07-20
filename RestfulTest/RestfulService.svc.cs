using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace RestfulTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ResftfulService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ResftfulService.svc or ResftfulService.svc.cs at the Solution Explorer and start debugging.
    public class RestfulService : IRestfulService
    {
        public UserDetails LoginJsonData(RequestLogin data)
        {
            if(data.UserId == "Admin" && data.Password == "123456")
            {
                string userName = "TEST";
                if (HttpContext.Current.Request.Headers["token"] == Guid.Empty.ToString())
                {
                    userName = "OKAY";
                }

                return new UserDetails()
                {
                    UserId = data.UserId,
                    UserName = userName,
                    UserToken = Guid.NewGuid().ToString()
                };
            }

            //HttpContext.Current.Response.Headers.Add("token", Guid.NewGuid().ToString());
            //HttpContext.Current.Response.Headers.Add("error", "NO");

            OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
            response.StatusCode = System.Net.HttpStatusCode.NoContent;
            response.StatusDescription = "It is forbidden";
            response.Headers.Add("token", Guid.NewGuid().ToString());
            response.Headers.Add("error", "NO");

            return new UserDetails()
            {
                UserId = data.UserId,
                UserName = "OKAY",
                UserToken = Guid.NewGuid().ToString()
            };

            //return null;
        }
    }
}
