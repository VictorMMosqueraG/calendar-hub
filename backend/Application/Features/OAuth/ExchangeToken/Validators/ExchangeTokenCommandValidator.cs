namespace Application.Features.OAuth.ExchangeToken.Validators;

using Application.Features.OAuth.ExchangeToken.Commands;
using FluentValidation;

public class ExchangeTokenCommandValidator : AbstractValidator<ExchangeTokenCommand>
{
    private static readonly string[] SupportedProviders = ["google"];

    public ExchangeTokenCommandValidator()
    {
        RuleFor(x => x.Provider)
            .NotEmpty()
            .Must(p => SupportedProviders.Contains(p!.ToLower()))
            .WithMessage("Provider not supported.");

        RuleFor(x => x.Code)
            .NotEmpty();
    }
}
