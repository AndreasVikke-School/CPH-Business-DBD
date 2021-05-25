using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Connectors;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class HBaseService : IDisposable
    {
        private readonly HBaseConnector hBaseConnector;
        private readonly HttpClient hbaseClient;

        public HBaseService(string hbaseIp) {
            hBaseConnector = new HBaseConnector(hbaseIp);
            hbaseClient = hBaseConnector.GetClient();
        }

        public async Task<string> GetString(string key) {
            var response = await hbaseClient.GetAsync($"/users/{key}/cf:e");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        // public async Task<bool> CreateMovie(WatchlistModel watchlistModel) {
        //     var test = await hbaseClient.PostAsync("/users/schema", new StringContent("<TableSchema name=\"users\"><ColumnSchema name=\"cf\" /></TableSchema>", Encoding.UTF8, "text/xml"));

        //     var data = $@"
        //         <CellSet>
        //             <Row key='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(key))}'>
        //                 <Cell column='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes("cf:e"))}'>{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(value))}</Cell>
        //             </Row>
        //         </CellSet>
        //     ";

        //     var test2 = await hbaseClient.PutAsync($"/users/{key}", new StringContent(data, Encoding.UTF8, "text/xml"));
        //     return true;
        // }

        // public async Task<bool> SetString(string key, string value) {
        //     var test = await hbaseClient.PostAsync("/users/schema", new StringContent("<TableSchema name=\"users\"><ColumnSchema name=\"cf\" /></TableSchema>", Encoding.UTF8, "text/xml"));

        //     var data = $@"
        //         <CellSet>
        //             <Row key='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(key))}'>
        //                 <Cell column='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes("cf:e"))}'>{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(value))}</Cell>
        //             </Row>
        //         </CellSet>
        //     ";

        //     var test2 = await hbaseClient.PutAsync($"/users/{key}", new StringContent(data, Encoding.UTF8, "text/xml"));
        //     return true;
        // }



        public void Dispose() {
            hBaseConnector.Dispose();
        }
    }
}