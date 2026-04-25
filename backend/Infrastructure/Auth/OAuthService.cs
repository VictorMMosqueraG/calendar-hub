namespace Infrastructure.Auth;

using Core.Dtos.AppSettingDto;
using Core.Ports.Auth;
using global::Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

public class OAuthService(
    HttpClient httpClient,
    IOptions<OAuthSettingsDto> oauthSettings
) : IOAuthService
{
    private readonly OAuthSettingsDto _settings = oauthSettings.Value;

    public string GetAuthorizationUrl(string providerName)
    {
        var provider = GetProviderSettings(providerName);

        var queryParams = new Dictionary<string, string>
        {
            { "client_id", provider.ClientId! },
            { "redirect_uri", provider.RedirectUri! },
            { "response_type", "code" },
            { "scope", provider.Scope! },
            { "access_type", "offline" },
            { "prompt", "consent" }
        };

        var queryString = string.Join("&", queryParams.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
        return $"{provider.AuthUrl}?{queryString}";
    }

    public async Task<string> ExchangeCodeForTokenAsync(
        string providerName,
        string code,
        CancellationToken cancellationToken = default)
    {
        var provider = GetProviderSettings(providerName);

        var payload = new Dictionary<string, string>
        {
            { "client_id", provider.ClientId! },
            { "client_secret", provider.ClientSecret! },
            { "code", code },
            { "redirect_uri", provider.RedirectUri! },
            { "grant_type", "authorization_code" }
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, provider.TokenUrl)
        {
            Content = new FormUrlEncodedContent(payload)
        };

        using var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var tokenResponse = await response.Content.ReadFromJsonAsync<OAuthTokenResponse>(cancellationToken: cancellationToken);

        return tokenResponse?.AccessToken
            ?? throw new InvalidOperationException($"No access token received from {providerName}.");
    }

    private OAuthProviderSettingsDto GetProviderSettings(string providerName)
    {
        return providerName.ToLower() switch
        {
            "google" => _settings.Google ?? throw new InvalidOperationException("Google OAuth not configured."),
            _ => throw new ArgumentException($"Unknown provider: {providerName}")
        };
    }
}

internal record OAuthTokenResponse(
    [property: JsonPropertyName("access_token")] string? AccessToken,
    [property: JsonPropertyName("token_type")] string? TokenType,
    [property: JsonPropertyName("expires_in")] int? ExpiresIn,
    [property: JsonPropertyName("refresh_token")] string? RefreshToken,
    [property: JsonPropertyName("scope")] string? Scope
);
