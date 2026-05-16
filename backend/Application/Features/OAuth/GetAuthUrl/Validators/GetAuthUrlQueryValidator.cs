namespace Application.Features.OAuth.GetAuthUrl.Validators;

using Application.Features.OAuth.GetAuthUrl.Queries;
using Core.Constants;
using Core.Messages;
using FluentValidation;

public class GetAuthUrlQueryValidator : AbstractValidator<GetAuthUrlQuery>
{
    private static readonly string[] SupportedProviders = [ProviderConstant.Google];

    public GetAuthUrlQueryValidator()
    {
        RuleFor(x => x.Provider)
            .NotEmpty()
            .WithMessage(Message.ProviderRequired)
            .Must(p => SupportedProviders.Contains(p!, StringComparer.OrdinalIgnoreCase))
            .WithMessage(Message.ProviderNotSupported);
    }
}
