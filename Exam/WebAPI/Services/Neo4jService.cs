using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Neo4j.Driver;
using WebAPI.Connectors;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class Neo4jService : IDisposable
    {
        private readonly Neo4jConnector neo4JConnector;
        private readonly ISession neo4jDatabase;

        public Neo4jService(string neo4jIp) {
            neo4JConnector = new Neo4jConnector(neo4jIp);
            neo4jDatabase = neo4JConnector.GetDatabase();
        }

        public string GetString(string key) {
            string query = $"MATCH (a:{key}) " +
                            "RETURN {value: a.value} as value";

            Dictionary<string, object> results = neo4jDatabase.WriteTransaction(tx =>
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

        public bool SetString(string key, string value) {
            var greeting = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"CREATE (a:{key}) " +
                                    "SET a.value = $value " +
                                    "RETURN a.value",
                    new {value});
                return result.Single()[0].As<string>();
            });
            return true;
        }

        public bool createMovie(MovieModel movieModel){
            var create = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($@"CREATE (m:Movie) 
                                    SET m.title = $title 
                                    SET m.releaseYear = $releaseYear 
                                    SET m.description = $description 
                                    RETURN m.release", new {title = movieModel.Title,
                                                            releaseYear = movieModel.ReleaseYear, 
                                                            description = movieModel.Description});
                return result.Single()[0].As<string>();
            });
            return true; 
            
        }

        public string GetMovie(String movieTitle){
            Dictionary<string, object> results = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"MATCH (m:Movie)--(g:Genre) WHERE m.title = $movieTitle return " + 
                                    "{title: m.title, release: m.releaseYear, description: m.description, genre: g.genre} as Movie", new{movieTitle = movieTitle});
                return (Dictionary<string, object>)result.Single()[0];
            });
            // New Query for genre.
            // MATCH (Movie {title: "Saw_5"})--(Genre) RETURN {title: Movie.title, release: Movie.releaseYear, description: Movie.description, genre: Genre.genre}
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            byte[] json = JsonSerializer.SerializeToUtf8Bytes(results, options);
            return System.Text.Encoding.UTF8.GetString(json);
        }

        public string GetAllMovies(){
            List<Dictionary<string, object>> results = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"MATCH (m:Movie)-->(g:Genre) RETURN " + 
                                    "{title: m.title, release: m.releaseYear, description: m.description, genre: g.genre} as Movie");
                return result.Select(r => (Dictionary<string, object>) r[0]).ToList();
            });
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            byte[] json = JsonSerializer.SerializeToUtf8Bytes(results, options);
            return System.Text.Encoding.UTF8.GetString(json);
        }

        public bool CreateGenre(string genreName){
            var create = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($@"CREATE (g:Genre) 
                                    SET g.genre = $genre 
                                    RETURN g.genre", new {genre = genreName});
                return result.Single()[0].As<string>();
            });
            return true;
        }

        public bool SetGenreOnMovie(String movieTitle, String genreName){
            var create = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($@"MATCH (m:Movie), (g:Genre) 
                                     WHERE m.title = $movieTitle
                                     AND g.genre = $genreName
                                     MERGE (m)-[:Genre]->(g)", new {movieTitle = movieTitle, genreName = genreName});
                return result;
            });

            return true; 
        }

        public string getMoviesByGenre(string genreName){
            List<Dictionary<string, object>> results = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"MATCH (m:Movie)--(g:Genre) " +
                                    "WHERE g.genre = $genre " + 
                                    "return {genre: g.genre, movie: m.title, release: m.releaseYear, description: m.description}",
                                    new {genre = genreName});
                return result.Select(r => (Dictionary<string, object>) r[0]).ToList();
            });
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            byte[] json = JsonSerializer.SerializeToUtf8Bytes(results, options);
            return System.Text.Encoding.UTF8.GetString(json);
        }

        public void Dispose() {
            neo4jDatabase.Dispose();
        }
    }
}