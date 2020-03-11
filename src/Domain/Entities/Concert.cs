using Cloudbash.Domain.Events;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Entities
{
    public class Concert : Aggregate
    {
        public Concert(string title)
        {
            Id = Guid.NewGuid();
            RaiseEvent(new ConcertCreatedEvent(Id, title));
        }

        public string Title { get; set; }

        internal void Apply(ConcertCreatedEvent ev)
        {
            Id = ev.AggregateId;
            Title = ev.Title;
        }


    }
}
