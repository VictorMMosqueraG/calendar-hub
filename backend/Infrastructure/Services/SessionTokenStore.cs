namespace Infrastructure.Services;

using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

public class SessionTokenStore(IHttpContextAccessor httpContextAccessor) : ITokenStore
{
    private const string KeyPrefix = "CalendarToken_";

    private static readonly string[] Providers = ["Google"];

    public void SetToken(string providerName, string accessToken)
        => httpContextAccessor.HttpContext?.Session.SetString(GetKey(providerName), accessToken);

    public string? GetToken(string providerName)
        => httpContextAccessor.HttpContext?.Session.GetString(GetKey(providerName));

    public void RemoveToken(string providerName)
        => httpContextAccessor.HttpContext?.Session.Remove(GetKey(providerName));

    public Dictionary<string, bool> GetConnectedProviders()
        => Providers.ToDictionary(p => p, p => !string.IsNullOrEmpty(GetToken(p)));

    private static string GetKey(string providerName)
        => $"{KeyPrefix}{providerName.ToLower()}";
}
