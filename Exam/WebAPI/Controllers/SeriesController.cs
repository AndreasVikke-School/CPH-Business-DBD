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
        private readonly string _redisIp;

        public SeriesController(ILogger<SeriesController> logger, IConfiguration config){
            _logger = logger;
            _config = config;
            _hbaseIp = _config.GetValue<string>("hbase-ip");
            _neo4jIp = _config.GetValue<string>("neo4j-ip");
            _redisIp = _config.GetValue<string>("redis-ip");
        }

        [HttpPost("create")]
        public async Task<bool> createSeries(SeriesModel seriesModel){
            using(ChacheService chacheService = new ChacheService(_redisIp))
            using(LogService redisService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await redisService.CreateLog(Request, seriesModel);

                chacheService.FlushChache(ChacheTypes.Series);
                chacheService.FlushChache(ChacheTypes.SeriesByGenre);

                return neo4jService.createSeries(seriesModel);
            }
        }
        
        [HttpGet("get/{seriesTitle}")]
        public async Task<string> getSeries(string seriesTitle){
            using(LogService redisService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await redisService.CreateLog(Request, seriesTitle);
                return neo4jService.GetSeries(seriesTitle);
            }
        }

        [HttpGet("get/all")]
        public async Task<string> getAllSeries(){
            using(ChacheService chacheService = new ChacheService(_redisIp))
            using(LogService redisService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await redisService.CreateLog(Request, "");

                var chache = chacheService.GetChache(ChacheTypes.Series);
                if(chache != null)
                    return chache;

                var series = neo4jService.GetAllSeries();
                chacheService.CreateChache(ChacheTypes.Series, series);
                return series;
            }
        }

        [HttpGet("get/allByGenre/{genreName}")]
        public async Task<string> getSeriesByGenre(string genreName){
            using(ChacheService chacheService = new ChacheService(_redisIp))
            using(LogService redisService = new LogService(_hbaseIp))
            using(Neo4jService neo4jService = new Neo4jService(_neo4jIp)) {
                await redisService.CreateLog(Request, "");

                var chache = chacheService.GetChache(ChacheTypes.SeriesByGenre);
                if(chache != null)
                    return chache;

                var series = neo4jService.getSeriesByGenre(genreName);
                chacheService.CreateChache(ChacheTypes.Series, series);
                return series;
            }
        }

        
    } 
}