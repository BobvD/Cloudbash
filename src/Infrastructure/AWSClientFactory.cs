﻿using Amazon.Runtime;
using Cloudbash.Infrastructure.Configs;
using System;
namespace Cloudbash.Infrastructure
{
    public interface IAwsClientFactory<out T>
    {
        T GetAwsClient();
    }

    public class AwsClientFactory<T> : IAwsClientFactory<T> where T : AmazonServiceClient, new()
    {
        private readonly IAwsBasicConfiguration _awsBasicConfiguration;

        public AwsClientFactory(AwsBasicConfiguration awsBasicConfiguration)
        {
            _awsBasicConfiguration = awsBasicConfiguration;
        }

        public T GetAwsClient()
        {
            return string.IsNullOrEmpty(_awsBasicConfiguration.AccessKey) ||
                   string.IsNullOrEmpty(_awsBasicConfiguration.SecretKey)
              ? (T)Activator.CreateInstance(typeof(T))
              : (T)Activator.CreateInstance(typeof(T), _awsBasicConfiguration.GetAwsCredentials(),
                _awsBasicConfiguration.RegionEndpoint);
        }
    }
}