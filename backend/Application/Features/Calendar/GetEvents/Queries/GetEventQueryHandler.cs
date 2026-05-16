namespace Application.Features.Calendar.GetEvents.Queries;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Interfaces;
using Core.Dtos.ResponsesDto;
using Core.Messages;
using MediatR;

public class GetEventsQueryHandler(
    ICalendarEventRetriever calendarEventRetriever
) : IRequestHandler<GetEventsQuery, ResultDto<List<GetEventsResponseDto>>>
{
    private readonly ICalendarEventRetriever _calendarEventRetriever = calendarEventRetriever;
    public async Task<ResultDto<List<GetEventsResponseDto>>> Handle(
        GetEventsQuery request,
        CancellationToken cancellationToken)
    {
        var events = await _calendarEventRetriever.GetEventsAsync(request, cancellationToken);

        return new ResultDto<List<GetEventsResponseDto>>
        {
            Results = events,
            Message = Message.GetAllData
        };
    }
}
