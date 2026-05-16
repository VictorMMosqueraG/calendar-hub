namespace Application.Features.OAuth.Interfaces;

using Core.Dtos.AppSettingDto;

public interface IOAuthProviderResolver
{
    OAuthProviderSettingsDto ResolveByName(string providerName);
}
