using System;
using System.IO;
using Neo4j.Driver;

namespace web
{
    public class Setup
    {

        public static string RunSetup() {
            string path = @"Files/";

            string setup1 = File.ReadAllText($"{path}Setup1.txt");
            string setup2 = File.ReadAllText($"{path}Setup2.txt");

            using(var databaseConnector = new DatabaseConnector()) {
                using(var session = databaseConnector.GetSession()) {
                    session.WriteTransaction(tx => {
                        var result1 = tx.Run(setup1);
                        var result2 = tx.Run(setup2);
                        return "Done";
                    });
                }
            }

            return "Done";
        }
    }
}