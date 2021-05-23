using Npgsql;
using StackExchange.Redis;
using System;

namespace WebAPI.Connectors
{
    public class PostgresConnector : IDisposable
    {
        private readonly NpgsqlConnection connector;

        public PostgresConnector(string postgresIp) {
            connector = new NpgsqlConnection($"Host={postgresIp};Username=postgres;Password=1234;Database=netflix");
        }

        public NpgsqlConnection GetDatabase() {
            connector.Open();
            return connector;
        }

        public void Dispose() {
            connector.Close();
        }
    }
}