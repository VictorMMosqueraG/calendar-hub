namespace Application.Features.Calendar.GetEvents.Queries;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Services;
using Application.Interfaces.Wrappers;
using Core.Dtos.ResponsesDto;
using Core.Messages;
using MediatR;

public class GetEventsQueryHandler(
    ICalendarProvider calendarProvider,
    CalendarRequestService calendarRequestService,
    CalendarResponseService calendarResponseService
) : IRequestHandler<GetEventsQuery, ResultDto<List<GetEventsResponseDto>>>
{
    private readonly ICalendarProvider _calendarProvider = calendarProvider;
    private readonly CalendarRequestService _calendarRequestService = calendarRequestService;
    private readonly CalendarResponseService _calendarResponseService = calendarResponseService;

    public async Task<ResultDto<List<GetEventsResponseDto>>> Handle(
        GetEventsQuery request,
        CancellationToken cancellationToken)
    {
        var calendarRequest = _calendarRequestService.Build(request);

        if (calendarRequest is null)
            return ResultDto<List<GetEventsResponseDto>>.Success([]);

        var response = await _calendarProvider.GetEventsAsync(
            calendarRequest.Value.Url, calendarRequest.Value.AccessToken, cancellationToken);

        var events = _calendarResponseService.Map(response);

        var result = ResultDto<List<GetEventsResponseDto>>.Success(events);
        result.Message = Message.GetAllData;

        return result;
    }
}
