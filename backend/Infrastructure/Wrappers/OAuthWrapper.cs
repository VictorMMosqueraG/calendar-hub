namespace Infrastructure.Wrappers;

using Application.Features.OAuth.ExchangeToken.Dtos;
using Application.Interfaces.Wrappers;
using Infrastructure.Extensions;
using System.Text.Json;

public class OAuthWrapper(HttpClient httpClient) : IOAuthWrapper
{
    public async Task<string> ExchangeCodeForTokenAsync(
        string tokenUrl,
        Dictionary<string, string> payload,
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
        {
            Content = new FormUrlEncodedContent(payload)
        };

        //COMEBACK: implement Transfer error handling and retry logic
        var response = await httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var tokenResponse = JsonSerializer.Deserialize<OAuthTokenResponseDto>(json, JsonOptionsExtension.SnakeCaseOptions);

        return tokenResponse?.AccessToken
            ?? throw new InvalidOperationException("No access token received.");
    }
}
