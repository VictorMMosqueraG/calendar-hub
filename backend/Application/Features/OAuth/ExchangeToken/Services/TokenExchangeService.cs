namespace Application.Features.OAuth.ExchangeToken.Services;

using Application.Features.OAuth.ExchangeToken.Dtos;
using Application.Interfaces.Wrappers;
using AutoMapper;
using Core.Dtos.AppSettingDto;
using Microsoft.Extensions.Options;

public class TokenExchangeService(
    IOAuthWrapper oAuthWrapper,
    IOptions<OAuthSettingsDto> oauthSettings,
    IMapper mapper
)
{
    private readonly IOAuthWrapper _oAuthWrapper = oAuthWrapper;
    private readonly OAuthSettingsDto _settings = oauthSettings.Value;
    private readonly IMapper _mapper = mapper;

    public async Task<string> ExchangeAsync(
        string providerName,
        string code,
        CancellationToken cancellationToken)
    {
        var provider = ResolveProvider(providerName);
        var exchangeParams = _mapper.Map<GoogleTokenExchangeParamsDto>(provider);
        exchangeParams = exchangeParams with { Code = code };

        return await _oAuthWrapper.ExchangeCodeForTokenAsync(
            provider.TokenUrl!, exchangeParams.ToDictionary(), cancellationToken);
    }

    private OAuthProviderSettingsDto ResolveProvider(string providerName)
        => providerName.ToLower() switch
        {
            "google" => _settings.Google ?? throw new InvalidOperationException("Google OAuth not configured."),
            _ => throw new ArgumentException($"Unknown provider: {providerName}")
        };
}
