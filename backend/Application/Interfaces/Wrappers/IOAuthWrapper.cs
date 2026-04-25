namespace Application.Interfaces.Wrappers;

public interface IOAuthWrapper
{
    Task<string> ExchangeCodeForTokenAsync(string tokenUrl, Dictionary<string, string> payload, CancellationToken cancellationToken = default);
}
