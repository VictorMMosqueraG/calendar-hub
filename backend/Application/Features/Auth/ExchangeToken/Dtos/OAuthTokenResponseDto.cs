namespace Application.Features.Auth.ExchangeToken.Dtos;

public record OAuthTokenResponseDto
{
    public string? AccessToken { get; init; }
    public string? TokenType { get; init; }
    public int? ExpiresIn { get; init; }
    public string? RefreshToken { get; init; }
    public string? Scope { get; init; }
}
