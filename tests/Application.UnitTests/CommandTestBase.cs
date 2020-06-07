using Cloudbash.Application.Common.EventSourcing;
using Cloudbash.Domain.Concerts;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Application.UnitTests
{
    public class CommandTestBase
    {
        public CommandTestBase()
        {
            ConcertRepo = EventSourcedRepositoryFactory<Concert>.Create();
        }

        public EventSourcedRepository<Concert> ConcertRepo { get; }

        protected async Task<Concert> CreateAndSaveNewConcertAggregate()
        {
            var concert = new Concert ( 
                    null,
                    new Guid("2bfb53d2-549d-438b-a8be-e87a7457abd7"),
                    null
                );

            await ConcertRepo.SaveAsync(concert);

            return concert;
        }

    }
}