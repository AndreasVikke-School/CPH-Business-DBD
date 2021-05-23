using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAPI.Managers;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IConfiguration _config;
        private readonly string _postgresIp;
        private readonly string _redisIp;


        public ProfileController(ILogger<ProfileController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _redisIp = _config.GetValue<string>("redis-ip");
            _postgresIp = _config.GetValue<string>("postgres-ip");
        }

        [HttpGet("get/{id}")]
        public async Task<ProfileModel> GetProfile(int id)
        {
            using(RedisService redisService = new RedisService(_redisIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                redisService.CreateLog(Request, new { Id = id });
                return await postgresService.GetProfile(id);
            }
        }

        [HttpGet("get/all")]
        public async Task<List<ProfileModel>> GetAllProfiles()
        {
            using(RedisService redisService = new RedisService(_redisIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                redisService.CreateLog(Request, "");
                return await postgresService.GetAllProfiles();
            }
        }

        [HttpPost("create/{accountId}")]
        public async Task<ProfileModel> CreateAccount(int accountId, ProfileModel profileModel)
        {
            using(RedisService redisService = new RedisService(_redisIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                redisService.CreateLog(Request, profileModel);
                return await postgresService.CreateProfile(accountId, profileModel);
            }
        }

    }
}