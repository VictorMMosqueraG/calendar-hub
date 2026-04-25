namespace Application.Features.Auth.ExchangeToken.Dtos;

public record GoogleTokenExchangeParamsDto
{
    public string? ClientId { get; init; }
    public string? ClientSecret { get; init; }
    public string? Code { get; init; }
    public string? RedirectUri { get; init; }
    public string GrantType { get; init; } = "authorization_code";
}
