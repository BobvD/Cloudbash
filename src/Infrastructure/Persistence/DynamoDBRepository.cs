using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class DynamoDBRepository<T> : IViewModelRepository<T>, IDisposable where T : IReadModel
    {
        private readonly AmazonDynamoDBClient _amazonDynamoDBClient;
        private readonly DynamoDBOperationConfig _configuration;
        private readonly ILogger<DynamoDBRepository<T>> _logger;

        public DynamoDBRepository(
            IAwsClientFactory<AmazonDynamoDBClient> clientFactory, 
            ILogger<DynamoDBRepository<T>> logger)
        {
            _logger = logger;
            _amazonDynamoDBClient = clientFactory.GetAwsClient();
            _configuration = new DynamoDBOperationConfig
            {
                OverrideTableName = "Cloudbash" ,
                SkipVersionCheck = true
            };            
        }        

        public Task<List<T>> GetAllAsync()
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                var prefixLength = typeof(T).Name.Length;

                var conditions = new List<ScanCondition> {
                 new ScanCondition("Id", ScanOperator.BeginsWith, typeof(T).Name)
                };
                
                var result =  context.ScanAsync<T>(conditions, _configuration).GetRemainingAsync()
                    .Result.Select(t => { t.Id = t.Id.Remove(0, prefixLength); return t; }).ToList();
                return Task.FromResult(result);
            }
        }

        public Task<T> GetAsync(Guid id)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                try
                {   
                    var conditions = new List<ScanCondition> {
                     new ScanCondition("Id", ScanOperator.Equal, GenerateEntityID(id.ToString()))
                    };

                    var result = context.ScanAsync<T>(conditions, _configuration).GetRemainingAsync().Result.First<T>();
                    return Task.FromResult(result);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }              
               
            }

            return Task.FromResult(default(T));
        }

        public async Task<T> AddAsync(T entity)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                try
                {
                    entity.Id = GenerateEntityID(entity.Id);                    
                    await context.SaveAsync<T>(entity, _configuration);
                    return entity;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }
            return default(T);
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                try
                {
                    await context.DeleteAsync<T>(GenerateEntityID(id.ToString()), _configuration);                   
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }
        }

        private string GenerateEntityID(string id)
        {
            return typeof(T).Name + id;
        }

        public void Dispose()
        {
            _amazonDynamoDBClient.Dispose();
        }

        public Task<List<T>> GetAllAsync(string[] children)
        {
            return GetAllAsync();
        }

        public Task<List<T>> FilterAsync(Expression<Func<T, bool>> filter, string[] children)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Guid id, string[] children)
        {
            return GetAsync(id);
        }
    }
}
