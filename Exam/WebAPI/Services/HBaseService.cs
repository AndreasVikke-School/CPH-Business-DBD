using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HBaseNet;
using HBaseNet.HRpc;
using HBaseNet.HRpc.Descriptors;
using HBaseNet.Utility;
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

        public async Task<string> GetString(string key) {
            var getResult = (await hBaseConnector.GetStandardClient()).Get(new GetCall("test", key));

            return getResult.Result.ToString();
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

            var ct = (await hBaseConnector.GetAdminClient()).CreateTable(create);

            var values = new Dictionary<string, Dictionary<string, byte[]>>
            {
                {
                    "default", new Dictionary<string, byte[]>
                    {
                        {"key", value.ToUtf8Bytes()}
                    }
                }
            };
            var rs = (await hBaseConnector.GetStandardClient()).Put(new MutateCall(table, key, values));
            return true;
        }

        public void Dispose() {
            hBaseConnector.Dispose();
        }
    }
}