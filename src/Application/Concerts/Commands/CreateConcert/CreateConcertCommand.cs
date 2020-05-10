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
        public string VenueId { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }

        /// <summary>
        /// The Command Handler, receives the command from Mediatr and processes it.
        /// </summary>
        public class CreateConcertCommandHandler : IRequestHandler<CreateConcertCommand, Guid>
        {

            IRepository<Concert> _repository;

            public CreateConcertCommandHandler(IRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateConcertCommand request, CancellationToken cancellationToken)
            {   
                // Create 
                var concert = new Concert(request.Name, request.VenueId, request.ImageUrl, request.Date);
                // Save
                await _repository.SaveAsync(concert);
                // Return Id
                return concert.Id; 
            }
        }

    }
}
