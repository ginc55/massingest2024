using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
    class Loginauth
    {
        public static bool AuthorizeLogin(string Username, string Password)
        {
            string username = Username;//"tpssistema";
            string password = Password;//"TPSsys2014";
            string serviceUrl = "https://svc.proc.test.lndb.lv/";

            WebClient client = new WebClient();
            client.BaseAddress = serviceUrl;
            client.UseDefaultCredentials = true;

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            bool responseBool = false ;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            try
            {
                var response = client.DownloadString("CheckAuthorization");
                var responseToObject = JsonConvert.DeserializeObject<AuthorizationResponse>(response);
                responseBool = responseToObject.Result;
                
            }
            catch (WebException ex)
            {
                string error = ex.Message;
                string errorStatus = ex.Status.ToString();

                
            }

            return responseBool;
            

            

            

           

        }
    }
}
