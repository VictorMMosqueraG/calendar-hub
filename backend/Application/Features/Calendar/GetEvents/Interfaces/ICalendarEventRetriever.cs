namespace Application.Features.Calendar.GetEvents.Interfaces;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Queries;

public interface ICalendarEventRetriever
{
    Task<List<GetEventsResponseDto>> GetEventsAsync(
        GetEventsQuery query,
        CancellationToken cancellationToken);
}
