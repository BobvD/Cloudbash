using FluentValidation;

namespace Cloudbash.Application.Concerts.Commands.CreateTicketType
{
    public class CreateTicketTypeCommandValidator : AbstractValidator<CreateTicketTypeCommand>
    {
        public CreateTicketTypeCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");

            RuleFor(v => v.Quantity)
                .NotNull()
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be at least 1.");
            
            RuleFor(v => v.Price)
                .NotNull()
                .GreaterThanOrEqualTo(1).WithMessage("Price must be at least 1.");
        }

    }
}

