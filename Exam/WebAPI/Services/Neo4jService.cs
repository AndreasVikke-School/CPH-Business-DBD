using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Neo4j.Driver;
using StackExchange.Redis;
using WebAPI.Connectors;

namespace WebAPI.Services
{
    public class Neo4jService : IDisposable
    {
        private readonly Neo4jConnector neo4JConnector;
        private readonly ISession neo4jDatabase;

        public Neo4jService(string neo4jIp) {
            neo4JConnector = new Neo4jConnector(neo4jIp);
            neo4jDatabase = neo4JConnector.GetDatabase();
        }

        public string GetString(string key) {
            string query = $"MATCH (a:{key}) " +
                            "RETURN {value: a.value} as value";

            Dictionary<string, object> results = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run(query);
                return (Dictionary<string, object>)result.Single()[0];
            });
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            byte[] json = JsonSerializer.SerializeToUtf8Bytes(results, options);
            return System.Text.Encoding.UTF8.GetString(json);
        }

        public bool SetString(string key, string value) {
            var greeting = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"CREATE (a:{key}) " +
                                    "SET a.value = $value " +
                                    "RETURN a.value",
                    new {value});
                return result.Single()[0].As<string>();
            });
            return true;
        }

        public void Dispose() {
            neo4jDatabase.Dispose();
        }
    }
}