using Cloudbash.Domain.Users.Events;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.Users
{
    public class User : AggregateRootBase
    {
        private User()
        {
            Activities = new List<UserActivity>();
        }
        
        public string FullName { get; set; }
        public string Email { get; set; }
        private List<UserActivity> Activities { get; set; }
        public User(Guid id, string fullName, string email)
        {
            AddEvent(new UserCreatedEvent(id, fullName, email));
        }

        public void AddActivityLog(UserActivityType activity)
        {
            if(activity == UserActivityType.AUTHENTICATION)
            {
                AddEvent(new UserAuthenticatedEvent(Id));
            }
        }

        internal void Apply(UserCreatedEvent ev)
        {
            Id = ev.AggregateId;
            FullName = ev.FullName;
            Email = ev.Email;
        }

        internal void Apply(UserAuthenticatedEvent ev)
        {
            Activities.Add(
                new UserActivity { 
                    Activity = UserActivityType.AUTHENTICATION, 
                    UserID = ev.AggregateId, 
                    TimeStamp = DateTime.Now 
                });
        }
    }
}
