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
        private readonly string _hbaseIp;

        public LogController(ILogger<LogController> logger, IConfiguration config) {
            _logger = logger;
            _config = config;
            _hbaseIp = _config.GetValue<string>("hbase-ip");
        }

        [HttpGet("get/log/{method}/{path}")]
        public async Task<string> GetLog(string method, string path) {
            using(LogService redisService = new LogService(_hbaseIp)) {
                return await redisService.GetLog(Uri.UnescapeDataString(method), Uri.UnescapeDataString(path));
            }
        }

        // [HttpGet("get/log/all/{key_pattern}")]
        // public Dictionary<string, string>[] GetAllLogsByKeyPattern(string key_pattern) {
        //     using(RedisService redisService = new RedisService(_redisIp)) {
        //         return redisService.GetAllLogsByKeyPattern(Uri.UnescapeDataString(key_pattern));
        //     }
        // }

        [HttpGet("get/log/all")]
        public async Task<string> GetAllLogs() {
            using(LogService redisService = new LogService(_hbaseIp)) {
                return await redisService.GetAllLogs();
            }
        }
    }
}
