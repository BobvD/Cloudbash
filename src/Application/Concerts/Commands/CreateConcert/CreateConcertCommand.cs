using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Concerts.Commands.CreateConcert
{
    public class CreateConcertCommand : IRequest<long>
    {

        public string Name { get; set; }

        public class CreateConcertCommandHandler : IRequestHandler<CreateConcertCommand, long>
        {
           

            public CreateConcertCommandHandler()
            {
                
            }

            public async Task<long> Handle(CreateConcertCommand request, CancellationToken cancellationToken)
            {
                
                return 1;
            }
        }

    }
}
