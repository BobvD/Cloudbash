using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Commands.CreateConcert
{
    public class CreateConcertCommand : IRequest<Guid>
    {

        public string Name { get; set; }

        public class CreateConcertCommandHandler : IRequestHandler<CreateConcertCommand, Guid>
        {

            IRepository<Concert> _repository;

            public CreateConcertCommandHandler(IRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateConcertCommand request, CancellationToken cancellationToken)
            {                
                var concert = new Concert(request.Name);
                await _repository.SaveAsync(concert);

                return concert.Id; 
            }
        }

    }
}
