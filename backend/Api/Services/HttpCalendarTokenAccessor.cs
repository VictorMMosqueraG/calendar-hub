namespace Api.Services;

using Core.Ports.Auth;
using Core.Ports.Providers;

public class HttpCalendarTokenAccessor(ITokenStore tokenStore) : ICalendarTokenAccessor
{
    public string? GetToken(string providerName)
        => tokenStore.GetToken(providerName);
}
