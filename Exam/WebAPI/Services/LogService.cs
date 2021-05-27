using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StackExchange.Redis;
using WebAPI.Connectors;
using WebAPI.Managers;
using WebAPI.Models;
using static WebAPI.Models.HBaseResult;

namespace WebAPI.Services
{
    public class LogService : IDisposable
    {
        private readonly HBaseConnector hBaseConnector;
        private readonly HttpClient hbaseClient;

        public LogService(string hbaseIp) {
            hBaseConnector = new HBaseConnector(hbaseIp);
            hbaseClient = hBaseConnector.GetClient();
        }

        public async Task<bool> CreateLog(HttpRequest request, object data) {
            long unixTimeMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            string path = $"{request.Path}".Replace('/', '_');
            
            var table = await hbaseClient.PostAsync("/httplogs/schema", new StringContent($"<TableSchema name=\"httplogs\"><ColumnSchema name=\"{path}\" /></TableSchema>", Encoding.UTF8, "text/xml"));

            table.EnsureSuccessStatusCode();

            var log = $@"
                <CellSet>
                    <Row key='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Method))}'>
                        <Cell column='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{path}:{unixTimeMilliseconds}"))}'>
                            {System.Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)))}
                        </Cell>
                    </Row>
                </CellSet>
            ";

            var putLog = await hbaseClient.PutAsync($"/httplogs/{request.Method}", new StringContent(log, Encoding.UTF8, "text/xml"));
            if(putLog.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<HBaseResult> GetLog(string method, string path) {
            hbaseClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await hbaseClient.GetAsync($"/httplogs/{method}/{path.Replace('/', '_')}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<HBaseResult>(await response.Content.ReadAsStringAsync());
        }

        public async Task<HBaseResult> GetAllLogs() {
            hbaseClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await hbaseClient.GetAsync($"/httplogs/*");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<HBaseResult>(await response.Content.ReadAsStringAsync());
        }

        public void Dispose() {
            hbaseClient.Dispose();
        }
    }
}