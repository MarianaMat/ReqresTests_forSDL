using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReqresTests_forSDL;

namespace ReqresTestProject_SDL.controllers
{
    abstract class BaseController
    {
            public MyRestClient MyRestClient;
            public BaseController()
            {
             MyRestClient = new MyRestClient();
            }
        protected T GetDtoFromResponse<T>(IRestResponse response) {
            var jsonResponse = JObject.Parse(response.Content);
            return JsonConvert.DeserializeObject<T>(jsonResponse.ToString());
        }
        protected static void VerifyStatusCode(IRestResponse response, int expectedStatusCode)
        {
            var actualStatusCode = (int)response.StatusCode;
            if (actualStatusCode != expectedStatusCode)
            {
                throw new Exception(String.Format("Unsacessfull, Status Code is {0}", response.StatusCode.ToString()));
            }

        }
    }
}



