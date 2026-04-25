namespace Core.Ports.Auth;

public interface IOAuthService
{
    string GetAuthorizationUrl(string providerName);
    Task<string> ExchangeCodeForTokenAsync(string providerName, string code, CancellationToken cancellationToken = default);
}
