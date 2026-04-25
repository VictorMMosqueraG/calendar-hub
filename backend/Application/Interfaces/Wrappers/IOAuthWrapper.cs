namespace Application.Interfaces.Wrappers;

using Application.Features.OAuth.GetAuthUrl.Dtos;

public interface IOAuthWrapper
{
    GetAuthUrlResponseDto GetAuthorizationUrl(string providerName);
    Task<string> ExchangeCodeForTokenAsync(string providerName, string code, CancellationToken cancellationToken = default);
}
