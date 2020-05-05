using Amazon.S3;
using Amazon.S3.Model;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Infrastructure.Configs;
using System;

namespace Cloudbash.Infrastructure.Services
{
    public class S3FileService : IFileService
    {
        private readonly AmazonS3Client _amazonS3Client;
        private readonly IServerlessConfiguration _config;

        public S3FileService(IAwsClientFactory<AmazonS3Client> clientFactory,
                                  IServerlessConfiguration config)
        {
            _amazonS3Client = clientFactory.GetAwsClient();
            _config = config;
        }

        public string GetUploadUrl(string filename)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _config.S3BucketName,
                Key = filename,
                Verb = HttpVerb.PUT,
                Expires = DateTime.Now.AddMinutes(15)
            };

            string url = _amazonS3Client.GetPreSignedURL(request);
            return url;
        }
    }
}
