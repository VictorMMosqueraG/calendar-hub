namespace Application.Features.Calendar.GetEvents.Services;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Queries;
using Application.Features.Calendar.GetEvents.Interfaces;
using Application.Interfaces.Wrappers;

public class CalendarEventRetriever(
    ICalendarProvider calendarProvider,
    ICalendarRequestBuilder calendarRequestBuilder,
    ICalendarResponseMapper calendarResponseMapper
) : ICalendarEventRetriever
{
    private readonly ICalendarProvider _calendarProvider = calendarProvider;
    private readonly ICalendarRequestBuilder _calendarRequestBuilder = calendarRequestBuilder;
    private readonly ICalendarResponseMapper _calendarResponseMapper = calendarResponseMapper;

    public async Task<List<GetEventsResponseDto>> GetEventsAsync(
        GetEventsQuery query,
        CancellationToken cancellationToken)
    {
        var calendarRequest = _calendarRequestBuilder.Build(query);

        if (calendarRequest is null)
            return [];

        var (url, accessToken) = calendarRequest.Value;

        var response = await _calendarProvider.GetEventsAsync(url, accessToken, cancellationToken);

        return _calendarResponseMapper.Map(response);
    }
}
