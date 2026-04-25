namespace Application.Features.OAuth.GetAuthUrl.Validators;

using Application.Features.OAuth.GetAuthUrl.Queries;
using FluentValidation;

public class GetAuthUrlQueryValidator : AbstractValidator<GetAuthUrlQuery>
{
    private static readonly string[] SupportedProviders = ["google"];

    public GetAuthUrlQueryValidator()
    {
        RuleFor(x => x.Provider)
            .NotEmpty()
            .Must(p => SupportedProviders.Contains(p!.ToLower()))
            .WithMessage("Provider not supported.");
    }
}
