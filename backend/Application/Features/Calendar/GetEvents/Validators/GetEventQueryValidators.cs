namespace Application.Features.Calendar.GetEvents.Validators;

using Application.Features.Calendar.GetEvents.Queries;
using Core.Messages;
using FluentValidation;

public class GetEventsQueryValidator : AbstractValidator<GetEventsQuery>
{
    public GetEventsQueryValidator()
    {
        RuleFor(x => x.From)
            .NotEmpty()
            .WithMessage(Message.FromDateRequired);

        RuleFor(x => x.To)
            .NotEmpty()
            .WithMessage(Message.ToDateRequired)
            .GreaterThan(x => x.From)
            .WithMessage(Message.ToDateMustBeGreaterThanFrom);
    }
}
