namespace Application.Interfaces.Wrappers;

public interface IOAuthWrapper
{
    string GetAuthorizationUrl(string providerName);
    Task<string> ExchangeCodeForTokenAsync(string providerName, string code, CancellationToken cancellationToken = default);
}
