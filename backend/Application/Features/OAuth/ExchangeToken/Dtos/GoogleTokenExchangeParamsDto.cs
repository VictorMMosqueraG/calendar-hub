namespace Application.Features.OAuth.ExchangeToken.Dtos;

public record GoogleTokenExchangeParamsDto
{
    public string? ClientId { get; init; }
    public string? ClientSecret { get; init; }
    public string? Code { get; init; }
    public string? RedirectUri { get; init; }
    public string GrantType { get; init; } = "authorization_code";

    public Dictionary<string, string> ToDictionary()
        => new()
        {
            { "client_id", ClientId! },
            { "client_secret", ClientSecret! },
            { "code", Code! },
            { "redirect_uri", RedirectUri! },
            { "grant_type", GrantType }
        };
}
