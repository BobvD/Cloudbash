using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Commands.CreateConcert
{
    public class CreateConcertCommand : IRequest<long>
    {

        public string Name { get; set; }

        public class CreateConcertCommandHandler : IRequestHandler<CreateConcertCommand, long>
        {

            IPublisher _publiser;

            public CreateConcertCommandHandler(IPublisher publisher)
            {
                _publiser = publisher;
            }

            public async Task<long> Handle(CreateConcertCommand request, CancellationToken cancellationToken)
            {
                DomainEvent d = new DomainEvent();
                d.Id = new Guid();
                Console.WriteLine("Invoked");
                await _publiser.Publish(d);
                return _publiser.GetValue();
            }
        }

    }
}
