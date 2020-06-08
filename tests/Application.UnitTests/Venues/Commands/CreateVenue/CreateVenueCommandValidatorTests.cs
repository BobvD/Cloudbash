using Cloudbash.Application.Venues.Commands.CreateVenue;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cloudbash.Application.UnitTests.Venues.Commands.CreateVenue
{
    public class CreateVenueCommandValidatorTests
    {

        private CreateVenueCommandValidator validator;

        public CreateVenueCommandValidatorTests()
        {
            validator = new CreateVenueCommandValidator();
        }

        [Fact]
        public void Should_have_error_when_Name_is_null()
        {
            validator.ShouldHaveValidationErrorFor(venue => venue.Name, null as string);
        }

        [Fact]
        public void Should_not_have_error_when_name_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(venue => venue.Name, "Mumford & Sons");
        }

        [Fact]
        public void Should__have_error_when_name_is_longer_than_255_chars()
        {
            validator.ShouldHaveValidationErrorFor(venue => venue.Name,
                "QX2BDpjXuQDK4jPVEuJZWLl3w08KNKuhCtYWJ91yftlRmJmXUYM4ZmElQQJRNHK7h5lxk" +
                "2MXfMhE6u1sHe1WhCiBVXxr5QPiNRrUFU2Cg30BdwWTouPl0fO5N9rFU4TXPF62eqXpWeD9p" +
                "Y6MVyH7FLhXDaGLYOsyRxXAuEp7bT0R8sVLriuEVd9N6wWsvabM8sqQTPYZwBW" +
                "DQAlt9HVWmXpP62t2L4orNQNdATwyBDWrmQe8ie9Rdn5LC70PzyfC");
        }

        [Fact]
        public void Should_have_error_when_Address_is_null()
        {
            validator.ShouldHaveValidationErrorFor(venue => venue.Address, null as string);
        }

        [Fact]
        public void Should_not_have_error_when_Address_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(venue => venue.Address, "Amsterdam");
        }

    }
}