using Cloudbash.Domain.Events;
using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Entities
{
    public class User : AggregateRootBase
    {
        public User(string username, string email)
        {
            Id = Guid.NewGuid();
            AddEvent(new UserCreatedEvent(Id, username, email));
        }

        public string Username { get; set; }
        public string Email { get; set; }

        internal void Apply(UserCreatedEvent ev)
        {
            Id = ev.AggregateId;
            Username = ev.Username;
            Email = ev.Email;
        }
    }
}
