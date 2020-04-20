using Cloudbash.Infrastructure.Configs;
using StackExchange.Redis;
using System;

namespace Cloudbash.Infrastructure.Cache
{
    public class RedisCache : Cloudbash.Application.Common.Definitions.Cache
    {
        private ConnectionMultiplexer redisConnections;
        private string _connectionString;
        private IDatabase RedisDatabase
        {
            get
            {
                if (this.redisConnections == null)
                {
                    InitializeConnection();
                }
                return this.redisConnections != null ? this.redisConnections.GetDatabase() : null;
            }
        }

        public RedisCache(bool isCacheEnabled, IServerlessConfiguration config) : base(isCacheEnabled)
        {
            _connectionString = config.RedisConnectionString;
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            try
            {
                this.redisConnections = ConnectionMultiplexer.Connect(_connectionString);
            }
            catch (RedisConnectionException errorConnectionException)
            {
                Console.WriteLine("Error connecting the redis cache : " + errorConnectionException.Message, errorConnectionException);
            }
        }

        protected override string GetStringProtected(string key)
        {
            if (this.RedisDatabase == null)
            {
                return null;
            }
            var redisObject = this.RedisDatabase.StringGet(key);
            if (redisObject.HasValue)
            {
                return redisObject.ToString();
            }
            else
            {
                return null;
            }
        }

        protected override void SetStringProtected(string key, string objectToCache, TimeSpan? expiry = null)
        {
            if (this.RedisDatabase == null)
            {
                return;
            }

            this.RedisDatabase.StringSet(key, objectToCache, expiry);
        }

        protected override void DeleteProtected(string key)
        {
            if (this.RedisDatabase == null)
            {
                return;
            }
            this.RedisDatabase.KeyDelete(key);
        }

        protected override void FlushAllProtected()
        {
            if (this.RedisDatabase == null)
            {
                return;
            }
            var endPoints = this.redisConnections.GetEndPoints();
            foreach (var endPoint in endPoints)
            {
                var server = this.redisConnections.GetServer(endPoint);
                server.FlushAllDatabases();
            }
        }

        protected override void DeleteByPatternProtected(string key)
        {
            throw new NotImplementedException();
        }

        public override bool IsCacheRunning
        {
            get { return this.redisConnections != null && this.redisConnections.IsConnected; }
        }
    }
}
