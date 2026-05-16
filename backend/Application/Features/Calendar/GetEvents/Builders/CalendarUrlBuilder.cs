namespace Application.Features.Calendar.GetEvents.Builders;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Extensions;
using Application.Features.Calendar.GetEvents.Queries;
using Application.Features.Calendar.GetEvents.Interfaces;
using AutoMapper;
using Core.Dtos.AppSettingDto;
using Microsoft.Extensions.Options;

public class CalendarUrlBuilder(
    IOptions<CalendarSettingsDto> calendarSettings,
    IMapper mapper
) : ICalendarUrlBuilder
{
    private readonly CalendarSettingsDto _settings = calendarSettings.Value;

    public string Build(GetEventsQuery query)
    {
        var queryParams = mapper.Map<GoogleCalendarQueryParamsDto>(query);
        return queryParams.ToUrl(_settings.GoogleBaseUrl!);
    }
}
