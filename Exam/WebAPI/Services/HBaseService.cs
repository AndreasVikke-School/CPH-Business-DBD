using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task<string> GetString(string profileId, string movieId) {
            hbaseClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await hbaseClient.GetAsync($"/watchlist/{profileId}/{movieId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> CreateMovie(WatchlistModel watchlistModel) {
            string id = watchlistModel.TypeId.ToString();
            var test = await hbaseClient.PostAsync("/watchlist/schema", new StringContent($"<TableSchema name=\"watchlist\"><ColumnSchema name=\"{id}\" /></TableSchema>", Encoding.UTF8, "text/xml"));

            var start = $@"
                <CellSet>
                    <Row key='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(watchlistModel.ProfileId.ToString()))}'>
                        <Cell column='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}:type"))}'>{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(watchlistModel.Type.ToString()))}</Cell>
                        <Cell column='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}:timestamp"))}'>{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(watchlistModel.TimeStamp.ToString()))}</Cell>";
            
            string end;
            if(watchlistModel.Episode != null && watchlistModel.Season != null) {
                end = $@"
                        <Cell column='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}:season"))}'>{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(watchlistModel.Season.ToString()))}</Cell>
                        <Cell column='{System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}:episode"))}'>{System.Convert.ToBase64String(Encoding.UTF8.GetBytes(watchlistModel.Episode.ToString()))}</Cell>
                    </Row>
                </CellSet>
            ";
            } else {
                end = $@"
                        </Row>
                </CellSet>
            ";
            }

            var test2 = await hbaseClient.PutAsync($"/watchlist/{watchlistModel.ProfileId.ToString()}", new StringContent(start+end, Encoding.UTF8, "text/xml"));
            return true;
        }

        public void Dispose() {
            hBaseConnector.Dispose();
        }
    }
}