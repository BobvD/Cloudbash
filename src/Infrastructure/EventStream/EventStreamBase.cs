using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.EventStream
{
    public abstract class EventStreamBase : IPublisher
    {

        protected string CreateEnveloppe(IDomainEvent @event)
        {
            var enveloppe = new
            {
                Event = Serialize(@event),
                Type = @event.GetType()
            };

            return Serialize(enveloppe);
        }

        private static string Serialize(Object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public abstract Task PublishAsync(IDomainEvent domainEvent);
    }
}
