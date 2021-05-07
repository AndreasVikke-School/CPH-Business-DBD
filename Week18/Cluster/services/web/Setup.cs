using System;
using System.IO;
using Neo4j.Driver;

namespace web
{
    public class Setup : IDisposable
    {
        private readonly IDriver _driver;

        public Setup()
        {
            _driver = GraphDatabase.Driver("neo4j://core1:7687", AuthTokens.Basic("neo4j", "1234"));
        }

        public string RunSetup() {
            string path = @"Files/";

            string setup1 = File.ReadAllText($"{path}Setup1.txt");
            Console.WriteLine(setup1);
            string setup2 = File.ReadAllText($"{path}Setup2.txt");
            Console.WriteLine(setup2);

            using(var session = _driver.Session()) {
                session.WriteTransaction(tx => {
                    var result1 = tx.Run(setup1);
                    var result2 = tx.Run(setup2);
                    return "Done";
                });
            }

            return "Done";
        }

        public void Dispose() {
            _driver?.Dispose();
        }
    }
}