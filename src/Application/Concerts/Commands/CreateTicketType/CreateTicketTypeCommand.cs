

using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Concerts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Commands.CreateTicketType
{
    public class CreateTicketTypeCommand : IRequest<Guid>
    {
        public Guid ConcertId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public class CreateTicketTypeCommandHandler : IRequestHandler<CreateTicketTypeCommand, Guid>
        {

            IRepository<Concert> _repository;

            public CreateTicketTypeCommandHandler(IRepository<Concert> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
            {               

                var concert = await _repository.GetByIdAsync(request.ConcertId);

                if (concert == null)
                {
                    throw new NotFoundException(nameof(Concert), request.ConcertId);
                }

                var ticketType = new TicketType
                {
                    Name = request.Name,
                    Price = request.Price,
                    Quantity = request.Quantity
                };

                concert.AddTicketType(ticketType);

                await _repository.SaveAsync(concert);

                return ticketType.Id;
                
            }
        }
    }
}
