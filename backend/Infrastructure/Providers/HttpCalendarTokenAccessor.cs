namespace Infrastructure.Providers;

using Application.Interfaces.Services;
using Core.Ports.Providers;

public class HttpCalendarTokenAccessor(ITokenStore tokenStore) : ICalendarTokenAccessor
{
    public string? GetToken(string providerName)
        => tokenStore.GetToken(providerName);
}
