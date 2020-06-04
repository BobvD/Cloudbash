using Cloudbash.Domain.UnitTests.Common;
using Cloudbash.Domain.Users;
using Cloudbash.Domain.Users.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cloudbash.Domain.UnitTests
{
    public class UserTests : AggregateRootBaseTest<User>
    {
        private readonly Guid userId = Guid.NewGuid();
        private readonly Guid ticketTypeId = Guid.NewGuid();

        [Fact]
        public void When_Create_New_User_UserCreatedEvent()
        {
            var user = new User(userId, "John Doe", "john@mail.com");

            AssertSingleUncommittedEvent<UserCreatedEvent>(user, @event =>
            {
                Assert.Equal(user.Id, @event.AggregateId);
                Assert.Equal(user.FullName, @event.FullName);
                Assert.Equal(user.Email, @event.Email);
                Assert.NotEqual(default(Guid), @event.AggregateId);
            });
        }

        [Fact]
        public void Given_User_When_Activity_Of_Type_AUTHENTICATION_New_UserAuthenticatedEvent()
        {
            var user = new User(userId, "John Doe", "john@mail.com");

            ClearUncommittedEvents(user);

            user.AddActivityLog(UserActivityType.AUTHENTICATION);

            AssertSingleUncommittedEvent<UserAuthenticatedEvent>(user, @event =>
            {                
                Assert.Equal(user.Id, @event.AggregateId);
            });
        }

    }
}
