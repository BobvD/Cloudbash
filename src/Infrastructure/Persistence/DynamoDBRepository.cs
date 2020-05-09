﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class DynamoDBRepository<T> : IViewModelRepository<T>, IDisposable where T : IReadModel
    {
        private readonly AmazonDynamoDBClient _amazonDynamoDBClient;
        private readonly DynamoDBOperationConfig _configuration;

        public DynamoDBRepository(IAwsClientFactory<AmazonDynamoDBClient> clientFactory)
        {
            _amazonDynamoDBClient = clientFactory.GetAwsClient();
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
                return await context.LoadAsync<T>(GenerateEntityID(id.ToString()), _configuration);
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            using (var context = new DynamoDBContext(_amazonDynamoDBClient))
            {
                try
                {
                    entity.Id = GenerateEntityID(entity.Id); 
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
                    await context.DeleteAsync<T>(GenerateEntityID(id.ToString()), _configuration);                   
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
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

    }
}