namespace Application.Features.OAuth.ExchangeToken.Extensions;

using Application.Features.OAuth.ExchangeToken.Dtos;

public static class TokenExchangeExtensions
{
    public static Dictionary<string, string> ToDictionary(this GoogleTokenExchangeParamsDto dto)
        => new()
        {
            { "client_id", dto.ClientId ?? string.Empty },
            { "client_secret", dto.ClientSecret ?? string.Empty },
            { "code", dto.Code ?? string.Empty },
            { "redirect_uri", dto.RedirectUri ?? string.Empty },
            { "grant_type", dto.GrantType ?? string.Empty }
        };
}
