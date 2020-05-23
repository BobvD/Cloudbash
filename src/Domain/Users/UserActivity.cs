using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Users
{
    public class UserActivity : EntityBase
    {
        public Guid UserID { get; set; }
        public DateTime TimeStamp { get; set; }
        public UserActivityType Activity { get; set; }

    }
}
