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
    public class Neo4jTest : ControllerBase
    {
        private readonly ILogger<Neo4jTest> _logger;
        private readonly IConfiguration _config;
        private readonly string _neo4jIp;

        public Neo4jTest(ILogger<Neo4jTest> logger, IConfiguration config) {
            _logger = logger;
            _config = config;
            _neo4jIp = _config.GetValue<string>("neo4j-ip");
        }

        [HttpGet("get/{key}")]
        public string Get(string key) {
            using(Neo4jService neo4JService = new Neo4jService(_neo4jIp)) {
                return neo4JService.GetString(key);
            }
        }

        [HttpPost("set/{key}/{value}")]
        public bool Post(string key, string value) {
            using(Neo4jService neo4JService = new Neo4jService(_neo4jIp)) {
                return neo4JService.SetString(key, value);
            }
        }
    }
}
