
using FluentValidation;

namespace Cloudbash.Application.Concerts.Commands.CreateConcert
{
    public class CreateConcertCommandValidator : AbstractValidator<CreateConcertCommand>
    {
        public CreateConcertCommandValidator()
        {

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");
            
        }

    }
}
