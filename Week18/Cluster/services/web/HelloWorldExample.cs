using System;
using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver;

namespace web
{
    public class HelloWorldExample
    {
        public static List<string> PrintGreeting(string message)
        {
            using(var databaseConnector = new DatabaseConnector()) {
                using (var session = databaseConnector.GetSession())
                {
                    var greeting = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("CREATE (a:Greeting) " +
                                            "SET a.message = $message " +
                                            "RETURN a.message + ', from node ' + id(a)",
                            new {message});
                        return result.Select(x => x[0].As<string>()).ToList();
                    });
                    return greeting;
                }
            }
            
        }
    }
}