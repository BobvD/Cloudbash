using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Venues;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Venues.Commands.CreateVenue
{
    public class CreateVenueCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string WebUrl { get; set; }
        public string Address { get; set; }

        /// <summary>
        /// The Command Handler, receives the command from Mediatr and processes it.
        /// </summary>
        public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, Guid>
        {

            IRepository<Venue> _repository;

            public CreateVenueCommandHandler(IRepository<Venue> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
            {
                // Create 
                var venue = new Venue(request.Name, request.Description, request.Capacity, request.WebUrl, request.Address);
                // Save
                await _repository.SaveAsync(venue);
                // Return Id
                return venue.Id;
            }
        }

    }
}
