﻿using FluentValidation;

namespace Cloudbash.Application.Concerts.Commands.RemoveTicketType
{
    public class RemoveTicketTypeCommandValidator : AbstractValidator<RemoveTicketTypeCommand>
    {
        public RemoveTicketTypeCommandValidator()
        {
            RuleFor(v => v.ConcertId)
                .NotNull()
                .NotEmpty();

            RuleFor(v => v.TicketTypeId)
                .NotNull()
                .NotEmpty();
        }

    }
}

