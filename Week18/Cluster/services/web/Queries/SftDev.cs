using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace web
{
    public class SftDev
    {
        public static string Run()
        {
            using(var databaseConnector = new DatabaseConnector()) {
                using (var session = databaseConnector.GetSession())
                {
                    Dictionary<string, object> results = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MATCH (sftDev:NodeModel)<-[:PART_OF]-(dom) " +
                                            "RETURN {nodeModel: sftDev.name, domain: collect(dom.name)} as SoftwareDevelopment"
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