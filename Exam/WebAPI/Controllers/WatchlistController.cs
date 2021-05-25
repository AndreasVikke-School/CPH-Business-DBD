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

        public WatchlistController(ILogger<WatchlistController> logger, IConfiguration config) {
            _logger = logger;
            _config = config;
            _hbaseIp = _config.GetValue<string>("hbase-ip");
        }

        [HttpGet("get/{profileId}/{movieId}")]
        public async Task<string> Get(string profileId, string movieId) {
            using(LogService redisService = new LogService(_hbaseIp))
            using(HBaseService hBaseService = new HBaseService(_hbaseIp)) {
                await redisService.CreateLog(Request, new { profileId, movieId });
                return await hBaseService.GetString(profileId, movieId);
            }
        }

        [HttpPost("create")]
        public async Task<bool> Post(WatchlistModel watchlistModel) {
            using(LogService redisService = new LogService(_hbaseIp))
            using(HBaseService hBaseService = new HBaseService(_hbaseIp)) {
                await redisService.CreateLog(Request, watchlistModel);
                return await hBaseService.CreateMovie(watchlistModel);
            }
        }
    }
}
