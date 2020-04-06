using Cloudbash.Domain.Events;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Entities
{
    public class Concert : AggregateRootBase
    {
        public Concert(string name)
        {
            Id = Guid.NewGuid();
            AddEvent(new ConcertCreatedEvent(Id, name));
        }

        public string Name { get; set; }

        internal void Apply(ConcertCreatedEvent ev)
        {
            Id = ev.AggregateId;
            Name = ev.Name;
        }

    }
}
