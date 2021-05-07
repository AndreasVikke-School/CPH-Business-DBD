using System;
using System.Linq;
using Neo4j.Driver;

namespace web
{
    public class HelloWorldExample : IDisposable
    {
        private readonly IDriver _driver;

        public HelloWorldExample(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public string PrintGreeting(string message)
        {
            using (var session = _driver.Session())
            {
                var greeting = session.WriteTransaction(tx =>
                {
                    var result = tx.Run("CREATE (a:Greeting) " +
                                        "SET a.message = $message " +
                                        "RETURN a.message + ', from node ' + id(a)",
                        new {message});
                    return result.Single()[0].As<string>();
                });
                return greeting;
            }
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        public static string Greet(string message) {
            using (var greeter = new HelloWorldExample("neo4j://core1:7687", "neo4j", "1234"))
            {
                string great = greeter.PrintGreeting("hello, world");
                return great;
            }
                
        }
    }
}