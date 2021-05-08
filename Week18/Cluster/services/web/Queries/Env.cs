using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace web
{
    public class Env
    {
        public static string Run()
        {
            using(var databaseConnector = new DatabaseConnector()) {
                using (var session = databaseConnector.GetSession())
                {
                    Dictionary<string, object> results = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MATCH (rel:Release)--(env:Environment { name: \"Break Fix Environment\" }) "+
                                            "RETURN {Rel:rel, Env:env} as Env"
                                            );
                        return (Dictionary<string, object>)result.Single()[0];
                    });
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    byte[] json = JsonSerializer.SerializeToUtf8Bytes(results, options);
                    return System.Text.Encoding.UTF8.GetString(json);
                }
            }
        }
    }
}