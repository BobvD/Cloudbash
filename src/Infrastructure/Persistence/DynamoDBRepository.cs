using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AutoMapper;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class DynamoDBRepository<T> : IViewModelRepository<T>, IDisposable where T : IReadModel
    {
        private readonly AmazonDynamoDBClient _amazonDynamoDBClient;
        private readonly DynamoDBOperationConfig _configuration;
        private readonly IMapper _mapper;

        public DynamoDBRepository(IAwsClientFactory<AmazonDynamoDBClient> clientFactory,
                                  IServerlessConfiguration config, 
                                  IMapper mapper)
        {
            _amazonDynamoDBClient = clientFactory.GetAwsClient();
            _mapper = mapper;
            _configuration = new DynamoDBOperationConfig
            {
                OverrideTableName = "Cloudbash" ,
                SkipVersionCheck = true
            };
            
        }        

        public async Task<List<T>> GetAllAsync()
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                var prefixLength = typeof(T).Name.Length;

                var conditions = new List<ScanCondition> {
                 new ScanCondition("Id", ScanOperator.BeginsWith, typeof(T).Name)
                };
                
                var x =  context.ScanAsync<T>(conditions, _configuration).GetRemainingAsync()
                    .Result.Select(t => { t.Id = t.Id.Remove(0, prefixLength); return t; }).ToList();
                return x;
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                return await context.LoadAsync<T>(id, _configuration);
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                try
                {
                    entity.Id = typeof(T).Name + entity.Id; 
                    await context.SaveAsync(entity, _configuration);
                    return entity;
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
            return default(T);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                try
                {
                    Console.WriteLine("Removing entity with id: " + id);
                    await context.DeleteAsync<T>(id, _configuration);                   
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }

        private IDynamoDBContext GetDynamoClient()
        {
            return new DynamoDBContext(_amazonDynamoDBClient);
        }
        
        public void Dispose()
        {
            _amazonDynamoDBClient.Dispose();
        }

    }
}
