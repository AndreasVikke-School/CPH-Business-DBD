using System;
using Neo4j.Driver;

namespace web
{
    public class DatabaseConnector : IDisposable
    {
        private readonly IDriver _driver;

        public DatabaseConnector()
        {
            _driver = GraphDatabase.Driver("neo4j://core1:7687", AuthTokens.Basic("neo4j", "1234"));
        }

        public ISession GetSession() {
            return _driver.Session();
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}