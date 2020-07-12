using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reqres_APITests.utils;
using RestSharp;
using System;

namespace Reqres_APITests.controllers
{
    abstract class BaseController
    {
        public MyRestClient MyRestClient;
        public BaseController()
        {
            MyRestClient = new MyRestClient();
        }
        protected T GetDtoFromResponse<T>(IRestResponse response)
        {
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



