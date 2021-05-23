using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using WebAPI.Connectors;
using WebAPI.Managers;

namespace WebAPI.Services
{
    public class RedisService : IDisposable
    {
        private readonly RedisConnector redisConnector;
        private readonly IDatabase redisDatabase;
        private readonly IServer redisServer;

        public RedisService(string redisIp) {
            redisConnector = new RedisConnector(redisIp);
            redisDatabase = redisConnector.GetDatabase();
            redisServer = redisConnector.GetServer();
        }

        public HashEntry[] CreateLog(HttpRequest request, object data) {
            long unixTimeMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            string path = $"{request.Path}";

            HashEntry[] methodLog = {
                new HashEntry("unixTime", unixTimeMilliseconds),
                new HashEntry("ip", ClientIpManager.GetClientIp(request)),
                new HashEntry("url", path),
                new HashEntry("method", request.Method),
                new HashEntry("data", JsonSerializer.Serialize(data))
            };
            redisDatabase.HashSet($"log{path}:{unixTimeMilliseconds}", methodLog);
            return redisDatabase.HashGetAll($"log/{path}:{unixTimeMilliseconds}");
        }

        public Dictionary<string, string> GetLog(string key) {
            return redisDatabase.HashGetAll(key)
                .ToDictionary(item => item.Name.ToString(), item => item.Value.ToString());
        }

        public Dictionary<string, string>[] GetAllLogsByKeyPattern(string key) {
            RedisKey[] keys = redisServer.Keys(pattern: key).ToArray();
            return keys.Select(key => redisDatabase.HashGetAll(key.ToString()))
                .ToArray()
                .Select(list => list.ToDictionary(item => item.Name.ToString(), item => item.Value.ToString()))
                .ToArray();
        }

        public Dictionary<string, string>[] GetAllLogs() {
            RedisKey[] keys = redisServer.Keys(pattern: "log/api/*").ToArray();
            return keys.Select(key => redisDatabase.HashGetAll(key.ToString()))
                .ToArray()
                .Select(list => list.ToDictionary(item => item.Name.ToString(), item => item.Value.ToString()))
                .ToArray();
        }

        public void Dispose() {
            redisConnector.Dispose();
        }
    }
}