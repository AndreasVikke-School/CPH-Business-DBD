using StackExchange.Redis;
using System;

namespace WebAPI.Connectors
{
    public class RedisConnector : IDisposable
    {
        private readonly ConnectionMultiplexer muxer;
        private readonly string _redisIp;

        public RedisConnector(string redisIp) {
            muxer = ConnectionMultiplexer.Connect($"{redisIp}:6379,password=1234");
            _redisIp = redisIp;
        }

        public IDatabase GetDatabase() {
            return muxer.GetDatabase();
        }

         public IServer GetServer() {
            return muxer.GetServer(_redisIp, 6379);
        }

        public void Dispose() {
            muxer.Close();
        }
    }
}