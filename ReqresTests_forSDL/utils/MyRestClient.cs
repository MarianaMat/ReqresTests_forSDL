using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReqresTests_forSDL
{
    class MyRestClient
    {
        public RestClient client;

        public MyRestClient()
        {
            this.client = new RestClient("https://reqres.in/");

        }

        public IRestResponse GET(String endpoint)
        {
            var request = BuildRequest(endpoint, Method.GET);
            return client.Execute(request);
        }
        public IRestResponse POST(String endpoint,Object body) 
        {
            var request = BuildRequest(endpoint, Method.POST);
            request.AddJsonBody(body);
            return client.Execute(request);
        }

        public IRestResponse PUT(String endpoint, Object body)
        {
            var request = BuildRequest(endpoint, Method.PUT);
            request.AddJsonBody(body);
            return client.Execute(request);
        }
        public IRestResponse PATCH(String endpoint, Object body)
        {
            var request = BuildRequest(endpoint, Method.PATCH);
            request.AddJsonBody(body);
            return client.Execute(request);
        }
        public IRestResponse DELETE(String endpoint)
        {
            var request = BuildRequest(endpoint, Method.DELETE);
            return client.Execute(request);
        }

        private RestRequest BuildRequest(String endpoint, Method method)
        {
            var request = new RestRequest(endpoint, method);
            return request;
        }


    }
}
