using Cloudbash.Application.Common.Exceptions;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Users;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Users.Commands.AddUserActivityLog
{
    public class AddUserActivityLogCommand : IRequest
    {
        public Guid UserId { get; set; }
        public UserActivityType ActivityType { get; set; }

        public class AddUserActivityLogCommandHandler : IRequestHandler<AddUserActivityLogCommand>
        {

            private readonly IRepository<User> _repository;

            public AddUserActivityLogCommandHandler(IRepository<User> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(AddUserActivityLogCommand request, CancellationToken cancellationToken)
            {               
                var user = await _repository.GetByIdAsync(request.UserId);

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.UserId);
                }              

                user.AddActivityLog(request.ActivityType);

                await _repository.SaveAsync(user);
                
                return Unit.Value;
            }
        }
    }
}



