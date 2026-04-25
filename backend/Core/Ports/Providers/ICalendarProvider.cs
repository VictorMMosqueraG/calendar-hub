namespace Core.Ports.Providers;

using Core.Entities;

public interface ICalendarProvider
{
    string ProviderName { get; }

    bool IsAvailable { get; }

    Task<IReadOnlyList<CalendarEvent>> GetEventsAsync(
        DateTime          from,
        DateTime          to,
        CancellationToken cancellationToken = default);
}
