using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Infrastructure.Configs;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;

namespace Cloudbash.Infrastructure.Cache
{
    public class RedisCache : ICache, IDisposable
    {
        private readonly IServerlessConfiguration _config;
        private IRedisClientsManager _manager;
        private Lazy<IRedisClient> _clientFactory;
        private readonly ILogger<RedisCache> _logger;

        public RedisCache(IServerlessConfiguration config,
                          ILogger<RedisCache> logger) 
        {
            _config = config;
            _logger = logger;
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            try
            {
                _clientFactory = new Lazy<IRedisClient>(GetRedisClient);
                _manager = new PooledRedisClientManager(_config.RedisConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }            
        }

        private IRedisClient GetRedisClient()
        {
            return _manager.GetClient();
        }

        private T Run<T>(Func<IRedisClient, T> action)
        {
            using (var client = GetRedisClient())
            {
                return action(client);
            }
        }

        public T Get<T>(Guid id) where T : class
        {            
            return Run(_ => _.As<T>().GetById(id));
        }

        public void Save<T>(T entity) where T : class
        {
            Run(_ => _.Store(entity));            
        }

        public void Dispose()
        {
            _manager.Dispose();
            if(_clientFactory.IsValueCreated)
            {
                _clientFactory.Value.Dispose();
            }            
        }

        public IList<T> Get<T>() where T : class
        {
            return Run(_ => _.As<T>().GetAll());
        }
    }
}
