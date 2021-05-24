using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly ILogger<GenreController> _logger;
        private readonly IConfiguration _config;
        private readonly string _neo4jIp;
        private readonly string _redisIp;

        public GenreController(ILogger<GenreController> logger, IConfiguration config){
            _logger = logger;
            _config = config;
            _redisIp = _config.GetValue<string>("redis-ip");
            _neo4jIp = _config.GetValue<string>("neo4j-ip");
        }

        [HttpPost("create/genre")]
        public bool createGenre(string genreName){
            using(RedisService redisService = new RedisService(_redisIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                redisService.CreateLog(Request, genreName);
                return neo4jService.CreateGenre(genreName);
            }
        }

        [HttpPost("set/genre")]
        public bool setGenreOnMovie(string movieTitle, string genreName){
            using(RedisService redisService = new RedisService(_redisIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                redisService.CreateLog(Request, movieTitle + " and " + genreName);
                return neo4jService.SetGenreOnMovie(movieTitle, genreName);
            }
        }
    }
}
