namespace Application.Features.Calendar.GetEvents.Validators;

using Application.Features.Calendar.GetEvents.Queries;
using FluentValidation;

public class GetEventsQueryValidator : AbstractValidator<GetEventsQuery>
{
    public GetEventsQueryValidator()
    {
        RuleFor(x => x.From)
            .NotEmpty()
            .WithMessage("La fecha de inicio es requerida.");

        RuleFor(x => x.To)
            .NotEmpty()
            .WithMessage("La fecha de fin es requerida.")
            .GreaterThan(x => x.From)
            .WithMessage("La fecha de fin debe ser mayor a la fecha de inicio.");
    }
}
