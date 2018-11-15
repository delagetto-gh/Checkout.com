using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Checkout.P1.BasketManagement.Application.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Checkout.P2.BasketManagementClient
{
    public abstract class BasketHttpClient : IDisposable
    {
        private readonly string baseUri;

        private HttpClient httpClient;

        private HttpClient HttpClient
        {
            get
            {
                if (this.httpClient == null)
                {
                    this.httpClient = new HttpClient();
                    this.httpClient.BaseAddress = new Uri(this.baseUri);
                }
                return this.httpClient;
            }
        }

        protected BasketHttpClient(string baseUri)
        {
            this.baseUri = baseUri;
        }

        protected HttpResponseMessage Post(string uri, object content = null)
        {
            return this.SendRequest(HttpMethod.Post, uri, content);
        }

        protected HttpResponseMessage Get(string uri, object content = null)
        {
            return this.SendRequest(HttpMethod.Get, uri, content);

        }

        protected HttpResponseMessage Put(string uri, object content = null)
        {
            return this.SendRequest(HttpMethod.Put, uri, content);
        }

        protected HttpResponseMessage Delete(string uri, object content = null)
        {
            return this.SendRequest(HttpMethod.Delete, uri, content);
        }

        protected string GenerateRequestUri(params string[] uriParams)
        {
            var allUriParams = new List<string>() { this.baseUri };
            allUriParams.AddRange(uriParams);
            var fullUri = string.Join("/", allUriParams);
            return fullUri;
        }

        private HttpResponseMessage SendRequest(HttpMethod method, string uri, object content)
        {
            FormUrlEncodedContent formUrlEncodedContent = null;

            if (content != null)
            {
                var param = JsonConvert.SerializeObject(content);
                JObject paramsObj = JObject.Parse(param);

                var urlParameters = new List<KeyValuePair<string, string>>();
                foreach (var p in paramsObj.Properties())
                {
                    var kvp = new KeyValuePair<string, string>(p.Name, p.Value.ToString());
                    urlParameters.Add(kvp);
                }

                formUrlEncodedContent = new FormUrlEncodedContent(urlParameters);
            }

            var response = this.HttpClient.SendAsync(new HttpRequestMessage(method, uri) { Content = formUrlEncodedContent }).Result;

            return response;
        }

        public void Dispose()
        {
            this.httpClient.Dispose();
        }
    }
}