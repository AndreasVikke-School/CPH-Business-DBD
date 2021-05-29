using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using WebAPI.Connectors;
using WebAPI.Managers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class ChacheService : IDisposable
    {
        private readonly RedisConnector redisConnector;
        private readonly IDatabase redisDatabase;

        public ChacheService(string hbaseIp) {
            redisConnector = new RedisConnector(hbaseIp);
            redisDatabase = redisConnector.GetDatabase();
        }

        public bool CreateChache(ChacheTypes chacheType, string chacheData, string genreName = null) {
            if(genreName != null)
                return redisDatabase.StringSet($"chache:{chacheType.ToString()}#{genreName}", chacheData);
            return redisDatabase.StringSet($"chache:{chacheType.ToString()}", chacheData);
        }

        public string GetChache(ChacheTypes chacheType, string genreName = null) {
            if(genreName != null)
                return redisDatabase.StringGet($"chache:{chacheType.ToString()}#{genreName}");
            return redisDatabase.StringGet($"chache:{chacheType.ToString()}");
        }

        public bool FlushChache(ChacheTypes chacheType, string genreName = null) {
            if(genreName != null)
                return redisDatabase.KeyDelete($"chache:{chacheType.ToString()}#{genreName}");
            return redisDatabase.KeyDelete($"chache:{chacheType.ToString()}");
        }

        public void Dispose() {
            redisConnector.Dispose();
        }
    }
}