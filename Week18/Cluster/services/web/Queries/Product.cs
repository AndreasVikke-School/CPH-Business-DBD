using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace web
{
    public class Product
    {
        private static readonly string query = "MATCH(ctl:MvcController { name: \"ProductController\"} )-->(vw:MvcView) " +
                                            "MATCH (vw)-->(css:CssFile) " +
                                            "RETURN {controller: ctl.name, mvc_views: collect(vw.name), css_files: collect(css.name)} as controllers";
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