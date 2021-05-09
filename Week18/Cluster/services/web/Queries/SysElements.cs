using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace web
{
    public class SysElements
    {
        private static readonly string query = "MATCH (css:CssFile)--(vw:MvcView) " +
                                        "OPTIONAL MATCH (vw)--(ctl:MvcController) " +
                                        "RETURN {cssFile:  collect(DISTINCT css.name), " + 
                                        "mvcView: collect(DISTINCT vw.name), " +
                                        "mvcController: collect(DISTINCT ctl.name)} " +
                                        "as MvcViewElements;";
        public static string getElements()
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