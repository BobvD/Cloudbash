using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
        {

            IRepository<User> _repository;

            public CreateUserCommandHandler(IRepository<User> repository)
            {
                _repository = repository;
            }

            public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = new User(request.Username, request.Email);
                await _repository.SaveAsync(user);

                return user.Id;
            }
        }
    }
}
