using Cloudbash.Domain.SeedWork;
using Cloudbash.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.EventStore.Commands
{
    public class SaveEventCommand : IRequest<long>
    {
        public IDomainEvent Event { get; set; }

        public class SaveEventCommandHandler : IRequestHandler<SaveEventCommand, long>
        {

            private IEventStore _eventStore;

            public SaveEventCommandHandler(IEventStore eventStore)
            {
                _eventStore = eventStore;
            }

            public async Task<long> Handle(SaveEventCommand request, CancellationToken cancellationToken)
            {

                await _eventStore.SaveAsync(request.Event);
                return 1;
            }
        }

    }
}
