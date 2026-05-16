namespace Application.Features.Calendar.GetEvents.Interfaces;

using Application.Features.Calendar.GetEvents.Dtos;

public interface ICalendarResponseMapper
{
    List<GetEventsResponseDto> Map(GoogleCalendarResponseDto? response);
}
