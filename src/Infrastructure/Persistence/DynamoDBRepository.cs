using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Configs;
using System;
using System.Collections.Generic;
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
                OverrideTableName = "Cloudbash.Concerts",
                SkipVersionCheck = true
            };
            
        }        

        public async Task<List<T>> GetAllAsync()
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                var conditions = new List<ScanCondition>();
                return await context.ScanAsync<T>(conditions, _configuration).GetRemainingAsync();
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

        private T Run<T>(Func<IDynamoDBContext, T> action)
        {
            using (var client = GetDynamoClient())
            {
                return action(client);
            }
        }

        // THE GREATEST HACK IN HISTORY?
        private object Map(T t)
        {
            Type type = Type.GetType(this.GetType().Namespace + ".Dto." + t.GetType().Name + "," + Assembly.GetExecutingAssembly(), true);
            var entity = _mapper.Map(t, t.GetType(), type);
            return entity;
        }


        public T CastObject<T>(object input)
        {
            return (T)input;
        }

        public void Dispose()
        {
            _amazonDynamoDBClient.Dispose();
        }

    }
}
