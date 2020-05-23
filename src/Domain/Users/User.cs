using Cloudbash.Domain.Users.Events;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Users
{
    public class User : AggregateRootBase
    {
        public User(Guid id, string fullName, string email)
        {
            AddEvent(new UserCreatedEvent(id, fullName, email));
        }

        public string FullName { get; set; }
        public string Email { get; set; }

        internal void Apply(UserCreatedEvent ev)
        {
            Id = ev.AggregateId;
            FullName = ev.FullName;
            Email = ev.Email;
        }
    }
}
