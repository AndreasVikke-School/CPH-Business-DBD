using System;
using System.Net.Http;

namespace WebAPI.Connectors
{
    public class HBaseConnector : IDisposable
    {
        private readonly HttpClient client;

        public HBaseConnector(string hbaseIp) {
            client = new HttpClient();
            client.BaseAddress = new Uri($"http://{hbaseIp}/");
        }

        public HttpClient GetClient () {
            return client;
        }

        public void Dispose() {
            client.Dispose();
        }
    }
}