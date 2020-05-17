

using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Commands.CreateTicketType
{
    public class CreateTicketTypeCommand : IRequest<bool>
    {
        public Guid ConcertId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public class CreateTicketTypeCommandHandler : IRequestHandler<CreateTicketTypeCommand, bool>
        {

            IRepository<Concert> _repository;

            public CreateTicketTypeCommandHandler(IRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<bool> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
            {
                Console.WriteLine("Create Ticket Type Command.");

                var concert = await _repository.GetByIdAsync(request.ConcertId);

                if (concert == null)
                {
                    throw new NotFoundException(nameof(Concert), request.ConcertId);
                }

                Console.WriteLine("concert retrieved: " + concert.Id + " - " + concert.Name);

                concert.AddTicketType(
                    new TicketType { 
                        Name = request.Name, 
                        Price = request.Price, 
                        Quantity = request.Quantity 
                    });

                await _repository.SaveAsync(concert);
                return true;
                
            }
        }
    }
}
