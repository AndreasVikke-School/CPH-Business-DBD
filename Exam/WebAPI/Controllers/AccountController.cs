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
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;
        private readonly string _postgresIp;
        private readonly string _redisIp;

        public AccountController(ILogger<AccountController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _redisIp = _config.GetValue<string>("redis-ip");
            _postgresIp = _config.GetValue<string>("postgres-ip");
        }

        [HttpPost("login")]
        public async Task<bool> Login(LoginModel loginModel)
        {
            using(RedisService redisService = new RedisService(_redisIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                redisService.CreateLog(Request, loginModel);
                return await postgresService.Login(loginModel);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<AccountModel> GetAccount(int id)
        {
            using(RedisService redisService = new RedisService(_redisIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                redisService.CreateLog(Request, new { Id = id });
                return await postgresService.GetAccount(id);
            }
        }

        [HttpGet("get/all")]
        public async Task<List<AccountModel>> GetAllAccounts()
        {
            using(RedisService redisService = new RedisService(_redisIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                redisService.CreateLog(Request, "");
                return await postgresService.GetAllAccounts();
            }
        }

        [HttpPost("create")]
        public async Task<AccountModel> CreateAccount(AccountModel accountModel)
        {
            using(RedisService redisService = new RedisService(_redisIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                redisService.CreateLog(Request, accountModel);
                return await postgresService.CreateAccount(accountModel);
            }
        }
    }
}