using System;
using System.Threading.Tasks;
using HBaseNet;
using HBaseNet.HRpc;
using HBaseNet.HRpc.Descriptors;
using WebAPI.Connectors;

namespace WebAPI.Services
{
    public class HBaseService : IDisposable
    {
        private readonly HBaseConnector hBaseConnector;
        // private readonly IAdminClient hbaseDatabase;

        public HBaseService(string hbaseIp) {
            hBaseConnector = new HBaseConnector(hbaseIp);
            // hbaseDatabase = hBaseConnector;
        }

        public string GetString(string key) {
            return "Test";
        }

        public async Task<bool> SetString(string key, string value) {
            var table = "test";
            var cols = new[]
            {
                new ColumnFamily("key")
                {
                    Compression = Compression.GZ,
                    KeepDeletedCells = KeepDeletedCells.TRUE
                },
                new ColumnFamily("value")
                {
                    Compression = Compression.GZ,
                    KeepDeletedCells = KeepDeletedCells.TTL,
                    DataBlockEncoding = DataBlockEncoding.PREFIX
                }
            };
            var create = new CreateTableCall(table, cols)
            {
                SplitKeys = new[] { "0", "5" }
            };

            var ct = (await hBaseConnector.GetDatabase()).CreateTable(create);
            return true;
        }

        public void Dispose() {
            hBaseConnector.Dispose();
        }
    }
}