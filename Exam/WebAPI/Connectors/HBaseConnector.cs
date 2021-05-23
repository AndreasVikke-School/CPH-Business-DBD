using HBaseNet;
using System;
using System.Threading.Tasks;

namespace WebAPI.Connectors
{
    public class HBaseConnector : IDisposable
    {
        private AdminClient adminClient;

        public HBaseConnector(string hbaseIp) {
            adminClient = new AdminClient($"{hbaseIp}");
        }

        public async Task<IAdminClient> GetDatabase() {
            return await adminClient.Build();
        }

        public void Dispose() {
            adminClient.Dispose();
        }
    }
}