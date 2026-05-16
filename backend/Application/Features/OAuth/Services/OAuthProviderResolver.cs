namespace Application.Features.OAuth.Services;

using Application.Features.OAuth.Interfaces;
using Core.Constants;
using Core.Dtos.AppSettingDto;
using Core.Messages;
using Microsoft.Extensions.Options;

public class OAuthProviderResolver(IOptions<OAuthSettingsDto> oauthSettings) : IOAuthProviderResolver
{
    private readonly OAuthSettingsDto _settings = oauthSettings.Value;

    public OAuthProviderSettingsDto ResolveByName(string providerName)
        => providerName.Equals(ProviderConstant.Google, StringComparison.OrdinalIgnoreCase)
            ? _settings.Google ?? throw new InvalidOperationException(Message.OAuthProviderNotConfigured(providerName))
            : throw new ArgumentException(Message.UnknownProvider(providerName));
}
