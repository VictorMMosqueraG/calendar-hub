namespace Application.Interfaces.Wrappers;

using Application.Features.Calendar.GetEvents.Dtos;

public interface ICalendarProvider
{
    Task<GoogleCalendarResponseDto?> GetEventsAsync(
        string url,
        string accessToken,
        CancellationToken cancellationToken = default);
}
