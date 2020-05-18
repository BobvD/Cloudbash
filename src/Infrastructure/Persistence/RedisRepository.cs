using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Configs;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class RedisRepository<T> : IViewModelRepository<T>, IDisposable where T : class, IReadModel
    {
        private IServerlessConfiguration _config;
        private IRedisClientsManager _manager;
        private Lazy<IRedisClient> _clientFactory;

        public RedisRepository(IServerlessConfiguration config)
        {
            _config = config;
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            try
            {
                _clientFactory = new Lazy<IRedisClient>(GetRedisClient);
                _manager = new PooledRedisClientManager(_config.RedisConnectionString);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
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

        public async Task<T> AddAsync(T entity)
        {
            Run(_ => _.Store(entity));
            return entity;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return Run(_ => _.As<T>().GetAll().ToList());
        }

        public async Task<T> GetAsync(Guid id)
        {
            return Run(_ => _.As<T>().GetById(id));
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _manager.Dispose();
            if (_clientFactory.IsValueCreated)
            {
                _clientFactory.Value.Dispose();
            }
        }

        public Task<List<T>> GetAllAsync(string[] children)
        {
            return GetAllAsync();
        }

        public Task<List<T>> Filter(Expression<Func<T, bool>> filter, string[] children)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Guid id, string[] children)
        {
            return GetAsync(id);
        }
    }
}
