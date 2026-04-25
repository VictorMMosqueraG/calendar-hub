namespace Application.Features.OAuth.GetAuthUrl.Services;

using Application.Features.OAuth.GetAuthUrl.Dtos;
using AutoMapper;
using Core.Dtos.AppSettingDto;
using Microsoft.Extensions.Options;

public class AuthUrlBuilderService(
    IOptions<OAuthSettingsDto> oauthSettings,
    IMapper mapper
)
{
    private readonly OAuthSettingsDto _settings = oauthSettings.Value;
    private readonly IMapper _mapper = mapper;

    public GetAuthUrlResponseDto Build(string providerName)
    {
        var provider = ResolveProvider(providerName);
        var authParams = _mapper.Map<GoogleAuthParamsDto>(provider);

        return new GetAuthUrlResponseDto { Url = authParams.ToUrl(provider.AuthUrl!) };
    }

    private OAuthProviderSettingsDto ResolveProvider(string providerName)
        => providerName.ToLower() switch
        {
            "google" => _settings.Google ?? throw new InvalidOperationException("Google OAuth not configured."),
            _ => throw new ArgumentException($"Unknown provider: {providerName}")
        };
}
