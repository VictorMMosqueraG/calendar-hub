namespace Application.Features.Auth.GetAuthUrl.Validators;

using Application.Features.Auth.GetAuthUrl.Queries;
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
