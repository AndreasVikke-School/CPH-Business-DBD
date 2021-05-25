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
    public class WatchlistController : ControllerBase
    {
        private readonly ILogger<WatchlistController> _logger;
        private readonly IConfiguration _config;
        private readonly string _hbaseIp;
        private readonly string _redisIp;

        public WatchlistController(ILogger<WatchlistController> logger, IConfiguration config) {
            _logger = logger;
            _config = config;
            _redisIp = _config.GetValue<string>("redis-ip");
            _hbaseIp = _config.GetValue<string>("hbase-ip");
        }

        [HttpGet("get/{profileId}/{movieId}")]
        public async Task<string> Get(string profileId, string movieId) {
            using(RedisService redisService = new RedisService(_redisIp))
            using(HBaseService hBaseService = new HBaseService(_hbaseIp)) {
                redisService.CreateLog(Request, new { profileId, movieId });
                return await hBaseService.GetString(profileId, movieId);
            }
        }

        [HttpPost("create")]
        public async Task<bool> Post(WatchlistModel watchlistModel) {
            using(RedisService redisService = new RedisService(_redisIp))
            using(HBaseService hBaseService = new HBaseService(_hbaseIp)) {
                redisService.CreateLog(Request, watchlistModel);
                return await hBaseService.CreateMovie(watchlistModel);
            }
        }
    }
}
