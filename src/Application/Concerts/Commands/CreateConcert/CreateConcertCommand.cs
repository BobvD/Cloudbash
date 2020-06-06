using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Commands.CreateConcert
{
    public class CreateConcertCommand : IRequest<Guid>
    {

        public string Name { get; set; }
        public Guid VenueId { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
       
        public class CreateConcertCommandHandler : IRequestHandler<CreateConcertCommand, Guid>
        {

            private readonly IRepository<Concert> _repository;

            public CreateConcertCommandHandler(IRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateConcertCommand request, 
                CancellationToken cancellationToken)
            {   
                
                var concert = new Concert(request.Name, request.VenueId, request.ImageUrl);
                
                await _repository.SaveAsync(concert);
               
                return concert.Id; 
            }
        }
    }
}
