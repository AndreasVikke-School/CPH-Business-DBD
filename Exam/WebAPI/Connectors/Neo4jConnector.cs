using Neo4j.Driver;
using StackExchange.Redis;
using System;

namespace WebAPI.Connectors
{
    public class Neo4jConnector : IDisposable
    {
        private readonly IDriver _driver;

        public Neo4jConnector(string neo4jIp) {
            _driver = GraphDatabase.Driver($"{neo4jIp}:7689", AuthTokens.Basic("neo4j", "1234"));
        }

        public ISession GetDatabase() {
            return _driver.Session();
        }

        public void Dispose() {
            _driver?.Dispose();
        }
    }
}