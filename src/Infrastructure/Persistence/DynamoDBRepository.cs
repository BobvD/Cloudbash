using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Configs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class DynamoDBRepository<T> : IViewModelRepository<T>, IDisposable where T : ReadModelBase
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

        public async void Insert(T entity)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                try
                {
                    await context.SaveAsync(entity, _configuration);

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }

            /*
            Run(_ => _.SaveAsync(Map(entity), _configuration));
            */
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get()
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }             
        
        public void RemoveById(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
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
