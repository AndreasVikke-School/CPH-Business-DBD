using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using WebAPI.Connectors;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private readonly IConfiguration _config;
        private readonly string _redisIp;

        public LogController(ILogger<LogController> logger, IConfiguration config) {
            _logger = logger;
            _config = config;
            _redisIp = _config.GetValue<string>("redis-ip");
        }

        [HttpGet("get/log/{key}")]
        public Dictionary<string, string> GetLog(string key) {
            using(RedisService redisService = new RedisService(_redisIp)) {
                return redisService.GetLog(Uri.UnescapeDataString(key));
            }
        }

        [HttpGet("get/log/all/{key_pattern}")]
        public Dictionary<string, string>[] GetAllLogsByKeyPattern(string key_pattern) {
            using(RedisService redisService = new RedisService(_redisIp)) {
                return redisService.GetAllLogsByKeyPattern(Uri.UnescapeDataString(key_pattern));
            }
        }

        [HttpGet("get/log/all")]
        public Dictionary<string, string>[] GetAllLogs() {
            using(RedisService redisService = new RedisService(_redisIp)) {
                return redisService.GetAllLogs();
            }
        }
    }
}
