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

        public bool createMovie(MovieModel movieModel){
            int actorCount = 0; 
            string actorString = "";
            string actorRelationship = "";
            
            int directorCount = 0;
            string directorString = "";
            string directorRelationship = "";
            
            int writerCount = 0;
            string writerString = "";
            string writerRelationship = "";

            foreach (var actor in movieModel.actors)
            {
                actorString += "MERGE(a" + actorCount + ":Actor {name: '" + actor + "'}) ";
                actorRelationship += "MERGE (m)-[:Actor]->(a" + actorCount + ") ";
                actorCount++;
            }

            foreach (var director in movieModel.directors)
            {
                    directorString += "MERGE(d" + directorCount + ":Director {name: '" + director + "'}) ";
                    directorRelationship += "MERGE (m)-[:Director]->(d" + directorCount + ") ";
                    directorCount++;
            }

            foreach (var writer in movieModel.writers)
            {
                    writerString += "MERGE(w" + writerCount + ":Writer {name: '" + writer + "'}) ";
                    writerRelationship += "MERGE (m)-[:Writer]->(w" + writerCount + ") ";
                    writerCount++;
            }

            var create = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"CREATE (m:Movie) " +
                                    "SET m.title = $title " +
                                    "SET m.releaseYear = $releaseYear " +
                                    "SET m.description = $description " +
                                    "MERGE(g:Genre {genre: $genre}) " +
                                    actorString + directorString + writerString +
                                    actorRelationship + directorRelationship + writerRelationship +
                                    "MERGE (m)-[:Genre]->(g) " +
                                    "RETURN m.release", new {title = movieModel.Title,
                                                            releaseYear = movieModel.ReleaseYear, 
                                                            description = movieModel.Description,
                                                            genre = movieModel.genre});
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


        // SERIES METHODS
        public bool createSeries(SeriesModel seriesModel){
            int actorCount = 0; 
            string actorString = "";
            string actorRelationship = "";
            
            int directorCount = 0;
            string directorString = "";
            string directorRelationship = "";
            
            int writerCount = 0;
            string writerString = "";
            string writerRelationship = "";

            string seasonString = "";
            string seasonRelationship = "";

            string episodeString = "";
            string episodeRelationship = "";

            foreach (var actor in seriesModel.actors)
            {
                actorString += "MERGE(a" + actorCount + ":Actor {name: '" + actor + "'}) ";
                actorRelationship += "MERGE (s)<-[:ACTED_IN]-(a" + actorCount + ") ";
                actorCount++;
            }

            foreach (var director in seriesModel.directors)
            {
                    directorString += "MERGE(d" + directorCount + ":Director {name: '" + director + "'}) ";
                    directorRelationship += "MERGE (s)<-[:DIRECTOR_OF]-(d" + directorCount + ") ";
                    directorCount++;
            }

            foreach (var writer in seriesModel.writers)
            {
                    writerString += "MERGE(w" + writerCount + ":Writer {name: '" + writer + "'}) ";
                    writerRelationship += "MERGE (s)<-[:WRITER_OF]-(w" + writerCount + ") ";
                    writerCount++;
            }

            for (int season = 1; season <= seriesModel.seasons; season++)
            {
                    seasonString += "MERGE("+ seriesModel.Title+"Season" + season + ":Season {title: 'Season " + season + "', series:'"+seriesModel.Title+"'}) ";
                    seasonRelationship += "MERGE (s)<-[:SEASON_OF]-("+ seriesModel.Title+"Season" + season + ") ";
                    for (int episodeNum = 1; episodeNum <= 8; episodeNum++) 
                    {
                        episodeString += "MERGE("+ seriesModel.Title+"EpisodeS" + season + "E"+ episodeNum +":Episode {series:'"+seriesModel.Title+"', episode:'" + episodeNum + "', season:'"+season+"'}) ";
                        episodeRelationship += "MERGE ("+ seriesModel.Title+"Season" + season + ")<-[:EPISODE_OF]-("+ seriesModel.Title+"EpisodeS" + season + "E" + episodeNum + ") ";
                    }
            }

            var create = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"CREATE (s:Series) " +
                                    "SET s.title = $title " +
                                    "SET s.releaseYear = $releaseYear " +
                                    "SET s.description = $description " +
                                    "SET s.numOfSeasons =$numOfSeasons " +
                                    "MERGE(g:Genre {genre: $genre}) " +
                                    actorString + directorString + writerString + seasonString + episodeString + 
                                    actorRelationship + directorRelationship + writerRelationship + seasonRelationship + episodeRelationship +
                                    "MERGE (s)-[:Genre]->(g) " +
                                    "RETURN s.release", new {title = seriesModel.Title,
                                                            releaseYear = seriesModel.ReleaseYear, 
                                                            description = seriesModel.Description,
                                                            genre = seriesModel.genre,
                                                            numOfSeasons = seriesModel.seasons});
                return result.Single()[0].As<string>();
            });
            return true; 
            
        }

         public string GetSeries(String seriesTitle){
            Dictionary<string, object> results = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"MATCH (s:Series)--(g:Genre) WHERE s.title = $seriesTitle return " + 
                                    "{title: s.title, release: s.releaseYear, description: s.description, genre: g.genre, numOfSeasons: s.numOfSeasons} as Series", new{seriesTitle = seriesTitle});
                return (Dictionary<string, object>)result.Single()[0];
            });
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            byte[] json = JsonSerializer.SerializeToUtf8Bytes(results, options);
            return System.Text.Encoding.UTF8.GetString(json);
        }

        public string GetAllSeries(){
            List<Dictionary<string, object>> results = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"MATCH (s:Series)-->(g:Genre) RETURN " + 
                                    "{title: s.title, release: s.releaseYear, description: s.description, genre: g.genre, numOfSeasons: s.numOfSeasons} as Series");
                return result.Select(r => (Dictionary<string, object>) r[0]).ToList();
            });
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            byte[] json = JsonSerializer.SerializeToUtf8Bytes(results, options);
            return System.Text.Encoding.UTF8.GetString(json);
        }

        public string getSeriesByGenre(string genreName){
            List<Dictionary<string, object>> results = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run($"MATCH (s:Series)--(g:Genre) " +
                                    "WHERE g.genre = $genre " + 
                                    "return {genre: g.genre, series: s.title, release: s.releaseYear, description: s.description, numOfSeasons: s.numOfSeasons} as Series",
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

        public bool setupSeriesMovies(){

            var create = neo4jDatabase.WriteTransaction(tx =>
            {
                var result = tx.Run(@"
                                    //Series
                                    MERGE (theWitcher:Series {title: 'The Witcher', releaseYear: '2020', description: 'Blood and spatter', numOfSeasons: 1}) 
                                    MERGE (invincible:Series {title: 'Invincible', releaseYear: '2021', description: 'Exciting superhero story', numOfSeasons: 8}) 
                                    MERGE (friends:Series {title: 'Friends', releaseYear: '1994', description: 'A boring pack of friends', numOfSeasons: 10})  
                                    
                                    //Movies
                                    MERGE(pansLabyrint:Movies {title: 'Pans labyrint', releaseYear: '2006', description: 'Hvad fanden handler den om.'})
                                    MERGE(thor:Movies {title: 'Thor', releaseYear: '2016', description: 'Marvel Superhero Movie.'})
                                    MERGE(klovn:Movies {title: 'Klovn', releaseYear: '2010', description: 'Award winning danish comedy.'})

                                    //Series Actors
                                    MERGE (cavill:Actor {name: 'Henry Cavill', age: '38'})  
                                    MERGE (yeun:Actor {name: 'Steven Yeun', age: '38'})  
                                    MERGE (simmons:Actor {name: 'Jonathan Kimble Simmons', age: '66'})  
                                    MERGE (aniston:Actor {name: 'Jennifer Aniston', age: '52'})  

                                    //Movie Actors
                                    MERGE (hvam:Actor {name: 'Frank Hvam', age: '46'})  
                                    MERGE (hemsworth:Actor {name: 'Chris Hemsworth', age: '37'})  
                                    MERGE (baquero,:Actor {name: 'Ivana Baquero', age: '27'})  

                                    //Genres create
                                    MERGE (fantasy:Genre {genre:'Fantasy'})
                                    MERGE (superhero:Genre {genre:'Superhero'})
                                    MERGE (comedy:Genre {genre:'Comedy'})

                                    //Genres set relations

                                    //Series
                                    MERGE (fantasy)-[:GENRE_OF]->(theWitcher)
                                    MERGE (superhero)-[:GENRE_OF]->(invincible)
                                    MERGE (comedy)-[:GENRE_OF]->(friends)

                                    //Movies
                                    MERGE (fantasy)-[:GENRE_OF]->(pansLabyrint)
                                    MERGE (superhero)-[:GENRE_OF]->(thor)
                                    MERGE (comedy)-[:GENRE_OF]->(klovn)

                                    //End genre relationship 


                                    //Actors set relations

                                    //Series
                                    MERGE (cavill)-[:ACTED_IN]->(theWitcher)  
                                    MERGE (simmons)-[:ACTED_IN]->(invincible)  
                                    MERGE (yeun)-[:ACTED_IN]->(invincible) 
                                    MERGE (aniston)-[:ACTED_IN]->(friends)  

                                    //Movies
                                    MERGE (hvam)-[:ACTED_IN]->(klovn)  
                                    MERGE (hemsworth)-[:ACTED_IN]->(thor)  
                                    MERGE (baquero)-[:ACTED_IN]->(pansLabyrint) 

                                    //End actor relationship

                                    //Foreach loop for seasons episodes
                                    WITH [invincible, theWitcher, friends] as seriesList  
                                    FOREACH(series in seriesList |   
                                    FOREACH(seasonNum in range(1, series.numOfSeasons) |  
                                            MERGE (s:Season {series: series.title, season:seasonNum})-[:SEASON_OF]->(series)  
                                            FOREACH(episodeNum in range(1, 8) |
                                                MERGE (e:Episode {series: series.title, episode:episodeNum, season: seasonNum})-[:EPISODE_OF]->(s))))
                                                RETURN seriesList");
                return result.Single()[0].As<string>();
            });
            return true; 
            
        }


        public void Dispose() {
            neo4jDatabase.Dispose();
        }
    }
}