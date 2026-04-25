namespace Application.Features.Auth.Disconnect.Validators;

using Application.Features.Auth.Disconnect.Commands;
using FluentValidation;

public class DisconnectCommandValidator : AbstractValidator<DisconnectCommand>
{
    private static readonly string[] SupportedProviders = ["google"];

    public DisconnectCommandValidator()
    {
        RuleFor(x => x.Provider)
            .NotEmpty()
            .Must(p => SupportedProviders.Contains(p!.ToLower()))
            .WithMessage("Provider not supported.");
    }
}
