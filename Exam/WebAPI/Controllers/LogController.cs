using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using WebAPI.Connectors;
using WebAPI.Models;
using WebAPI.Services;
using static WebAPI.Models.HBaseResult;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private readonly IConfiguration _config;
        private readonly string _hbaseIp;
        private readonly string _redisIp;

        public LogController(ILogger<LogController> logger, IConfiguration config) {
            _logger = logger;
            _config = config;
            _hbaseIp = _config.GetValue<string>("hbase-ip");
            _redisIp = _config.GetValue<string>("redis-ip");
        }

        [HttpGet("get/{method}/{path}")]
        public async Task<HBaseResult> GetLog(string method, string path) {
            using(LogService logService = new LogService(_hbaseIp)) {
                return await logService.GetLog(Uri.UnescapeDataString(method), Uri.UnescapeDataString(path));
            }
        }

        [HttpGet("get/all")]
        public async Task<HBaseResult> GetAllLogs() {
            using(ChacheService chacheService = new ChacheService(_redisIp))
            using(LogService logService = new LogService(_hbaseIp)) {
                return await logService.GetAllLogs();
            }
        }
    }
}
