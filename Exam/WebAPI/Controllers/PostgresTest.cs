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
    public class PostgresTest : ControllerBase
    {
        private readonly ILogger<PostgresTest> _logger;
        private readonly IConfiguration _config;
        private readonly string _PostgresIp;

        public PostgresTest(ILogger<PostgresTest> logger, IConfiguration config) {
            _logger = logger;
            _config = config;
            _PostgresIp = _config.GetValue<string>("postgres-ip");
        }

        // [HttpGet("get/{key}")]
        // public async Task<string> Get(string key) {
        //     using(PostgresService postgresService = new PostgresService(_PostgresIp)) {
        //         return await postgresService.GetString(key);
        //     }
        // }

        // [HttpPost("set/{key}/{value}")]
        // public async Task<bool> Post(string key, string value) {
        //     using(PostgresService postgresService = new PostgresService(_PostgresIp)) {
        //         return await postgresService.SetString(key, value);
        //     }
        // }
    }
}
