namespace Application.Features.OAuth.ExchangeToken.Validators;

using Application.Features.OAuth.ExchangeToken.Commands;
using Core.Constants;
using Core.Messages;
using FluentValidation;

public class ExchangeTokenCommandValidator : AbstractValidator<ExchangeTokenCommand>
{
    private static readonly string[] SupportedProviders = [ProviderConstant.Google];

    public ExchangeTokenCommandValidator()
    {
        RuleFor(x => x.Provider)
            .NotEmpty()
            .WithMessage(Message.ProviderRequired)
            .Must(p => SupportedProviders.Contains(p!, StringComparer.OrdinalIgnoreCase))
            .WithMessage(Message.ProviderNotSupported);

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage(Message.CodeRequired);
    }
}
