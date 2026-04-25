namespace Core.Ports.Providers;

public interface ICalendarTokenAccessor
{
    string? GetToken(string providerName);
}
