using Cloudbash.Domain.Events;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Entities
{
    public class Concert : AggregateRootBase
    {
        private Concert() { }

        public Concert(string name, string venue, string imageUrl, string date)
        {
            Id = Guid.NewGuid();
            AddEvent(new ConcertCreatedEvent(Id, name, venue, imageUrl, date));
        }

        public string Name { get; set; }
        public string Venue { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }

        internal void Apply(ConcertCreatedEvent ev)
        {
            Id = ev.AggregateId;
            Name = ev.Name;
            Venue = ev.Venue;
            ImageUrl = ev.ImageUrl;
            Date = ev.Date;
        }

    }
}
