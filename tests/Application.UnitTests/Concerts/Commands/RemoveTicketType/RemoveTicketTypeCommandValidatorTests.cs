using Cloudbash.Application.Concerts.Commands.RemoveTicketType;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Commands.RemoveTicketType
{
    public class RemoveTicketTypeCommandValidatorTests
    {

        private RemoveTicketTypeCommandValidator validator;

        public RemoveTicketTypeCommandValidatorTests()
        {
            validator = new RemoveTicketTypeCommandValidator();
        }

        [Fact]
        public void Should_have_error_when_ConcertId_is_null()
        {
            validator.ShouldHaveValidationErrorFor(type => type.ConcertId, Guid.Empty);
        }

        [Fact]
        public void Should_not_have_error_when_ConcertId_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(type => type.ConcertId, Guid.NewGuid());
        }


        [Fact]
        public void Should_have_error_when_TicketTypeId_is_null()
        {
            validator.ShouldHaveValidationErrorFor(type => type.TicketTypeId, Guid.Empty);
        }

        [Fact]
        public void Should_not_have_error_when_TicketTypeId_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(type => type.TicketTypeId, Guid.NewGuid());
        }

    }
}