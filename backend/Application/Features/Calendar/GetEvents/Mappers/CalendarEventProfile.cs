namespace Application.Features.Calendar.GetEvents.Mappers;

using AutoMapper;
using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Queries;
using Core.Constants;
using System.Globalization;

public class CalendarEventProfile : Profile
{
    public CalendarEventProfile()
    {
        CreateMap<GetEventsQuery, GoogleCalendarQueryParamsDto>()
            .ForMember(dest => dest.TimeMin, opt => opt.MapFrom(src => src.From.ToUniversalTime()))
            .ForMember(dest => dest.TimeMax, opt => opt.MapFrom(src => src.To.ToUniversalTime()));

        CreateMap<GoogleCalendarEventDto, GetEventsResponseDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Summary ?? "(Sin título)"))
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => ParseDateTime(src.Start)))
            .ForMember(dest => dest.End, opt => opt.MapFrom(src => ParseDateTime(src.End)))
            .ForMember(dest => dest.IsAllDay, opt => opt.MapFrom(src => src.Start!.Date != null))
            .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Organizer != null ? src.Organizer.Email : null))
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(_ => ProviderConstant.Google));
    }

    private static DateTime ParseDateTime(GoogleCalendarTimeDto? time)
    {
        var value = time?.DateTime ?? time?.Date;

        return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result)
            ? result
            : DateTime.MinValue;
    }
}
