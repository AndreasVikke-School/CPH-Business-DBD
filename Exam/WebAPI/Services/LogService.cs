using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using WebAPI.Connectors;
using WebAPI.Managers;

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
            
            // var table = await hbaseClient.PostAsync("/httplogs/schema", new StringContent($"<TableSchema name=\"httplogs\"><ColumnSchema name=\"{path}\" /></TableSchema>", Encoding.UTF8, "text/xml"));
            var table = await hbaseClient.PostAsync("/httplogs/schema", new StringContent($"<TableSchema name=\"httplogs\"><ColumnSchema name=\"{path}\" /></TableSchema>", Encoding.UTF8, "text/xml"));

            table.EnsureSuccessStatusCode();

            var log = $@"
                <CellSet>
                    <Row key='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Method))}'>
                        <Cell column='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{path}:{unixTimeMilliseconds}"))}'>
                            {System.Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)))}
                        </Cell>
                    </Row>
                </CellSet>
            ";

            var putLog = await hbaseClient.PutAsync($"/httplogs/{request.Method}", new StringContent(log, Encoding.UTF8, "text/xml"));
            if(putLog.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<string> GetLog(string method, string path) {
            hbaseClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await hbaseClient.GetAsync($"/httplogs/{method}/{path.Replace('/', '_')}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // public Dictionary<string, string>[] GetAllLogsByKeyPattern(string key) {
        //     RedisKey[] keys = redisServer.Keys(pattern: key).ToArray();
        //     return keys.Select(key => redisDatabase.HashGetAll(key.ToString()))
        //         .ToArray()
        //         .Select(list => list.ToDictionary(item => item.Name.ToString(), item => item.Value.ToString()))
        //         .ToArray();
        // }

        public async Task<string> GetAllLogs() {
            hbaseClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await hbaseClient.GetAsync($"/httplogs/*");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose() {
            hbaseClient.Dispose();
        }
    }
}