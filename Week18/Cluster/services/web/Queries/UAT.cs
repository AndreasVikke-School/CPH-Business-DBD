using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace web
{
    public class UAT
    {
        public static string Run()
        {
            using(var databaseConnector = new DatabaseConnector()) {
                using (var session = databaseConnector.GetSession())
                {
                    Dictionary<string, object> results = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MATCH (rel_v1_3:Release { name: \"Release v1.3\" }) " +
                                            "MATCH (uat:Environment { name: \"UAT Environment\"}) " +
                                            "MERGE (rel_v1_3)-[r_1_3:DEPLOYED_IN]->(uat) " +
                                            "RETURN {uat: uat.name, rel_v1_3: rel_v1_3.name, r_1_3:r_1_3.name} as uat"
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