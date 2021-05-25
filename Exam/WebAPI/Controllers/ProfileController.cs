using System.Collections.Generic;
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
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IConfiguration _config;
        private readonly string _postgresIp;
        private readonly string _hbaseIp;


        public ProfileController(ILogger<ProfileController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _hbaseIp = _config.GetValue<string>("hbase-ip");
            _postgresIp = _config.GetValue<string>("postgres-ip");
        }

        [HttpGet("get/{id}")]
        public async Task<ProfileModel> GetProfile(int id)
        {
            using(LogService redisService = new LogService(_hbaseIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                await redisService.CreateLog(Request, new { Id = id });
                return await postgresService.GetProfile(id);
            }
        }

        [HttpGet("get/all/{account_id}")]
        public async Task<List<ProfileModel>> GetAllProfiles(int account_id)
        {
            using(LogService redisService = new LogService(_hbaseIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                await redisService.CreateLog(Request, "");
                return await postgresService.GetAllProfilesByAccout(account_id);
            }
        }

        [HttpPost("create/{accountId}")]
        public async Task<ProfileModel> CreateAccount(int accountId, ProfileModel profileModel)
        {
            using(LogService redisService = new LogService(_hbaseIp))
            using(PostgresService postgresService = new PostgresService(_postgresIp)) {
                await redisService.CreateLog(Request, profileModel);
                return await postgresService.CreateProfile(accountId, profileModel);
            }
        }

    }
}