using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAPI.Connectors;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HBaseTest : ControllerBase
    {
        private readonly ILogger<HBaseTest> _logger;
        private readonly IConfiguration _config;
        private readonly string _hbaseIp;

        public HBaseTest(ILogger<HBaseTest> logger, IConfiguration config) {
            _logger = logger;
            _config = config;
            _hbaseIp = _config.GetValue<string>("hbase-ip");
        }

        [HttpGet("get/{key}")]
        public async Task<string> Get(string key) {
            using(HBaseService hBaseService = new HBaseService(_hbaseIp)) {
                return await hBaseService.GetString(key);
            }
        }

        // [HttpPost("set/{key}/{value}")]
        // public async Task<bool> Post(string key, string value) {
        //     using(HBaseService hBaseService = new HBaseService(_hbaseIp)) {
        //         return await hBaseService.SetString(key, value);
        //     }
        // }
    }
}
