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
    public class SeriesController : ControllerBase
    {
        private readonly ILogger<SeriesController> _logger;
        private readonly IConfiguration _config;
        private readonly string _neo4jIp;
        private readonly string _hbaseIp;

        public SeriesController(ILogger<SeriesController> logger, IConfiguration config){
            _logger = logger;
            _config = config;
            _hbaseIp = _config.GetValue<string>("hbase-ip");
            _neo4jIp = _config.GetValue<string>("neo4j-ip");
        }

        [HttpPost("create/series")]
        public async Task<bool> createSeries(SeriesModel seriesModel){
            using(LogService redisService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await redisService.CreateLog(Request, seriesModel);
                return neo4jService.createSeries(seriesModel);
            }
        }
        
        [HttpGet("get/series/{seriesTitle}")]
        public async Task<string> getSeries(string seriesTitle){
            using(LogService redisService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await redisService.CreateLog(Request, seriesTitle);
                return neo4jService.GetSeries(seriesTitle);
            }
        }

        [HttpGet("get/series/all")]
        public async Task<string> getAllSeries(){
            using(LogService redisService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await redisService.CreateLog(Request, "");
                return neo4jService.GetAllSeries();
            }
        }

        [HttpGet("get/series/allByGenre/{genreName}")]
        public async Task<string> getSeriesByGenre(string genreName){
            using(LogService redisService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await redisService.CreateLog(Request, "");
                return neo4jService.getSeriesByGenre(genreName);
            }
        }

        
    } 
}