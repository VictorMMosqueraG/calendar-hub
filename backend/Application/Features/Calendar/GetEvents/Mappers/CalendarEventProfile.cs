namespace Application.Features.Calendar.GetEvents.Mappers;

using AutoMapper;
using Application.Features.Calendar.GetEvents.Dtos;
using Core.Entities;

public class CalendarEventProfile : Profile
{
    public CalendarEventProfile()
    {
        CreateMap<CalendarEvent, GetEventsResponseDto>();
    }
}
