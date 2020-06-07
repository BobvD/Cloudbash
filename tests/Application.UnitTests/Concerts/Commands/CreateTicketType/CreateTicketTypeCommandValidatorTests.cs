using Cloudbash.Application.Concerts.Commands.CreateConcert;
using Cloudbash.Application.Concerts.Commands.CreateTicketType;
using FluentValidation.TestHelper;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Commands.CreateTicketType
{
    public class CreateTicketTypeCommandValidatorTests
    {

        private CreateTicketTypeCommandValidator validator;

        public CreateTicketTypeCommandValidatorTests()
        {
            validator = new CreateTicketTypeCommandValidator();
        }

        [Fact]
        public void Should_have_error_when_Name_is_null()
        {
            validator.ShouldHaveValidationErrorFor(type => type.Name, null as string);
        }

        [Fact]
        public void Should_not_have_error_when_name_is_specified()
        {
            validator.ShouldNotHaveValidationErrorFor(type => type.Name, "Regular Ticket");
        }

        [Fact]
        public void Should__have_error_when_name_is_longer_than_255_chars()
        {
            validator.ShouldHaveValidationErrorFor(type => type.Name,
                "QX2BDpjXuQDK4jPVEuJZWLl3w08KNKuhCtYWJ91yftlRmJmXUYM4ZmElQQJRNHK7h5lxk" +
                "2MXfMhE6u1sHe1WhCiBVXxr5QPiNRrUFU2Cg30BdwWTouPl0fO5N9rFU4TXPF62eqXpWeD9p" +
                "Y6MVyH7FLhXDaGLYOsyRxXAuEp7bT0R8sVLriuEVd9N6wWsvabM8sqQTPYZwBW" +
                "DQAlt9HVWmXpP62t2L4orNQNdATwyBDWrmQe8ie9Rdn5LC70PzyfC");
        }

        [Fact]
        public void Should_have_error_when_Price_is_negative()
        {
            validator.ShouldHaveValidationErrorFor(type => type.Price, -1);
        }

        [Fact]
        public void Should_not_have_error_when_Price_is_positive()
        {
            validator.ShouldNotHaveValidationErrorFor(type => type.Price, 39);
        }

        [Fact]
        public void Should_have_error_when_Quantity_is_negative()
        {
            validator.ShouldHaveValidationErrorFor(type => type.Price, -1);
        }

        [Fact]
        public void Should_not_have_error_when_Quantity_is_positive()
        {
            validator.ShouldNotHaveValidationErrorFor(type => type.Price, 5000);
        }

    }
}