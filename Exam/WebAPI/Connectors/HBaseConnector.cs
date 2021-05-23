using HBaseNet;
using System;
using System.Threading.Tasks;

namespace WebAPI.Connectors
{
    public class HBaseConnector : IDisposable
    {
        private AdminClient adminClient;
        private StandardClient standardClient;

        public HBaseConnector(string hbaseIp) {
            adminClient = new AdminClient($"{hbaseIp}");
            standardClient = new StandardClient($"{hbaseIp}");
        }

        public async Task<IAdminClient> GetAdminClient() {
            return await adminClient.Build();
        }

        public async Task<IStandardClient> GetStandardClient() {
            return await standardClient.Build();
        }

        public void Dispose() {
            adminClient.Dispose();
        }
    }
}