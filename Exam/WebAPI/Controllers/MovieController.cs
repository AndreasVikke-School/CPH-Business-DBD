using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IConfiguration _config;
        private readonly string _neo4jIp;
        private readonly string _hbaseIp;
        private readonly string _redisIp;


        public MovieController(ILogger<MovieController> logger, IConfiguration config){
            _logger = logger;
            _config = config;
            _hbaseIp = _config.GetValue<string>("hbase-ip");
            _neo4jIp = _config.GetValue<string>("neo4j-ip");
            _redisIp = _config.GetValue<string>("redis-ip");
        }

        [HttpPost("create")]
        public async Task<bool> createMovie(MovieModel movieModel){
            using(ChacheService chacheService = new ChacheService(_redisIp))
            using(LogService logService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await logService.CreateLog(Request, movieModel);

                chacheService.FlushChache(ChacheTypes.Movie);
                chacheService.FlushChache(ChacheTypes.MovieByGenre);

                return neo4jService.createMovie(movieModel);
            }
        }
        
        [HttpGet("get/{movieTitle}")]
        public async Task<string> getMovie(string movieTitle){
            using(LogService logService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await logService.CreateLog(Request, movieTitle);
                return neo4jService.GetMovie(movieTitle);
            }
        }

        [HttpGet("get/all")]
        public async Task<string> getAllMovies(){
            using(ChacheService chacheService = new ChacheService(_redisIp))
            using(LogService logService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await logService.CreateLog(Request, "");

                var chache = chacheService.GetChache(ChacheTypes.Movie);
                if(chache != null)
                    return chache;

                var movies = neo4jService.GetAllMovies();
                chacheService.CreateChache(ChacheTypes.Movie, movies);
                return movies;
            }
        }

        [HttpGet("get/allByGenre/{genreName}")]
        public async Task<string> getMoviesByGenre(string genreName) {
            using(ChacheService chacheService = new ChacheService(_redisIp))
            using(LogService logService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await logService.CreateLog(Request, "");

                var chache = chacheService.GetChache(ChacheTypes.MovieByGenre);
                if(chache != null)
                    return chache;

                var movies = neo4jService.getMoviesByGenre(genreName);
                chacheService.CreateChache(ChacheTypes.MovieByGenre, movies);
                return movies;
            }
        }
    }
}