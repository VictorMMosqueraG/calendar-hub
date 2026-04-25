namespace Core.Ports.Auth;

public interface ITokenStore
{
    void SetToken(string providerName, string accessToken);
    string? GetToken(string providerName);
    void RemoveToken(string providerName);
    Dictionary<string, bool> GetConnectedProviders();
}
