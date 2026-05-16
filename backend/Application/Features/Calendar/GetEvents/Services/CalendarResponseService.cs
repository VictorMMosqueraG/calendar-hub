namespace Application.Features.Calendar.GetEvents.Services;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Interfaces;
using AutoMapper;

public class CalendarResponseService(IMapper mapper) : ICalendarResponseMapper
{
    private readonly IMapper _mapper = mapper;

    public List<GetEventsResponseDto> Map(GoogleCalendarResponseDto? response)
        => response?.Items?
            .Select(_mapper.Map<GetEventsResponseDto>)
            .OrderBy(e => e.Start)
            .ToList() ?? [];
}
