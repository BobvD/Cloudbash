using Amazon.DynamoDBv2.DataModel;
using Cloudbash.Application.Common.Mappings;
using System;

namespace Cloudbash.Infrastructure.Persistence.Dto
{
    [DynamoDBTable("Cloudbash.Concerts")]
    public class Venue : IMapFrom<Domain.ViewModels.Venue>
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; } 
        [DynamoDBProperty]
        public string Description { get; set; }
        [DynamoDBProperty]
        public int Capacity { get; set; }
        [DynamoDBProperty]
        public string WebUrl { get; set; }
        [DynamoDBProperty]
        public string Address { get; set; }

        public Venue() { }

    }
}
