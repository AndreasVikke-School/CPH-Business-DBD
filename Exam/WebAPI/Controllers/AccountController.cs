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
        private readonly string _hbaseIp;

        public AccountController(ILogger<AccountController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _hbaseIp = _config.GetValue<string>("hbase-ip");
            _postgresIp = _config.GetValue<string>("postgres-ip");
        }

        [HttpPost("login")]
        public async Task<bool> Login(LoginModel loginModel)
        {
            using(LogService redisService = new LogService(_hbaseIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                await redisService.CreateLog(Request, loginModel);
                return await postgresService.Login(loginModel);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<AccountModel> GetAccount(int id)
        {
            using(LogService redisService = new LogService(_hbaseIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                await redisService.CreateLog(Request, new { Id = id });
                return await postgresService.GetAccount(id);
            }
        }

        [HttpGet("get/all")]
        public async Task<List<AccountModel>> GetAllAccounts()
        {
            using(LogService redisService = new LogService(_hbaseIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                await redisService.CreateLog(Request, "");
                return await postgresService.GetAllAccounts();
            }
        }

        [HttpPost("create")]
        public async Task<AccountModel> CreateAccount(AccountModel accountModel)
        {
            using(LogService redisService = new LogService(_hbaseIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                await redisService.CreateLog(Request, accountModel);
                return await postgresService.CreateAccount(accountModel);
            }
        }
    }
}