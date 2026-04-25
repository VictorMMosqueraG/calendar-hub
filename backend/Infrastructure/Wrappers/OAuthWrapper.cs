namespace Infrastructure.Wrappers;

using Application.Features.OAuth.ExchangeToken.Dtos;
using Application.Features.OAuth.GetAuthUrl.Dtos;
using AutoMapper;
using Core.Dtos.AppSettingDto;
using Application.Interfaces.Wrappers;
using Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using System.Text.Json;

public class OAuthWrapper(
    HttpClient httpClient,
    IOptions<OAuthSettingsDto> oauthSettings,
    IMapper mapper
) : IOAuthWrapper
{
    private readonly OAuthSettingsDto _settings = oauthSettings.Value;

    public GetAuthUrlResponseDto GetAuthorizationUrl(string providerName)
    {
        var provider = GetProviderSettings(providerName);
        var authParams = mapper.Map<GoogleAuthParamsDto>(provider);
        var queryParams = QueryBuilder.ToDictionary(authParams, JsonNamingPolicy.SnakeCaseLower);
        var url = QueryBuilder.BuildUrl(provider.AuthUrl!, queryParams);

        return new GetAuthUrlResponseDto { Url = url };
    }

    public async Task<string> ExchangeCodeForTokenAsync(
        string providerName,
        string code,
        CancellationToken cancellationToken = default)
    {
        var provider = GetProviderSettings(providerName);
        var exchangeParams = mapper.Map<GoogleTokenExchangeParamsDto>(provider);
        exchangeParams = exchangeParams with { Code = code };
        var payload = QueryBuilder.ToDictionary(exchangeParams, JsonNamingPolicy.SnakeCaseLower);

        using var request = new HttpRequestMessage(HttpMethod.Post, provider.TokenUrl)
        {
            Content = new FormUrlEncodedContent(payload)
        };

        //COMEBACK: implement Transfer error handling and retry logic
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var tokenResponse = JsonSerializer.Deserialize<OAuthTokenResponseDto>(json, JsonOptionsHelper.SnakeCaseOptions);

        return tokenResponse?.AccessToken
            ?? throw new InvalidOperationException($"No access token received from {providerName}.");
    }

    private OAuthProviderSettingsDto GetProviderSettings(string providerName)
        => providerName.ToLower() switch
        {
            "google" => _settings.Google ?? throw new InvalidOperationException("Google OAuth not configured."),
            _ => throw new ArgumentException($"Unknown provider: {providerName}")
        };
}
