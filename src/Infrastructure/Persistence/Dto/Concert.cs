using Amazon.DynamoDBv2.DataModel;
using Cloudbash.Application.Common.Mappings;
using System;

namespace Cloudbash.Infrastructure.Persistence.Dto
{
    [DynamoDBTable("Cloudbash.Concerts")]
    public class Concert : IMapFrom<Domain.ViewModels.Concert>
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public string Venue { get; set; }
        [DynamoDBProperty]
        public string ImageUrl { get; set; }
        [DynamoDBProperty]
        public string Date { get; set; }

        public Concert()
        {

        }

        public Concert(Concert concert)
        {
            Id = concert.Id;
            Name = concert.Name;
            Venue = concert.Venue;
            ImageUrl = concert.ImageUrl;
            Date = concert.Date;
        }
    }
}
