using FluentValidation;

namespace Cloudbash.Application.Venues.Commands.CreateVenue
{
    class CreateVenueCommandValidator : AbstractValidator<CreateVenueCommand>
    {
        public CreateVenueCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Address is required.");
        }
    }
}
