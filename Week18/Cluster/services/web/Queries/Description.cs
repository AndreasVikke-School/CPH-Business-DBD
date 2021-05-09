using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace web
{
    public class Description
    {
        private static readonly string query = "MATCH (rel_v1_3:Release { name: \"Release v1.3\" }) " +
                                            "OPTIONAL MATCH (rel_v1_3)--(checkin:CheckIn) " +
                                            "RETURN {Release: rel_v1_3.name, CheckIn: checkin.name, Description: COALESCE(checkin.description, \"No description provided\")} AS `Description`";
        public static string Run()
        {
            using(var databaseConnector = new DatabaseConnector()) {
                using (var session = databaseConnector.GetSession())
                {
                    Dictionary<string, object> results = session.WriteTransaction(tx =>
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
            }
        }

         public static string getQuery()
        {
            return  "Result from: " + query + "\n\n";
        }
    }
}