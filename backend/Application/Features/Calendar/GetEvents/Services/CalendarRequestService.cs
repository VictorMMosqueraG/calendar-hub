namespace Application.Features.Calendar.GetEvents.Services;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Queries;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Dtos.AppSettingDto;
using Microsoft.Extensions.Options;

public class CalendarRequestService(
    ITokenStore tokenStore,
    IOptions<CalendarSettingsDto> calendarSettings,
    IMapper mapper
)
{
    private readonly ITokenStore _tokenStore = tokenStore;
    private readonly CalendarSettingsDto _settings = calendarSettings.Value;
    private readonly IMapper _mapper = mapper;

    public (string Url, string AccessToken)? Build(GetEventsQuery query)
    {
        var accessToken = _tokenStore.GetToken("Google");

        if (string.IsNullOrEmpty(accessToken))
            return null;

        var queryParams = _mapper.Map<GoogleCalendarQueryParamsDto>(query);
        var url = queryParams.ToUrl(_settings.GoogleBaseUrl!);

        return (url, accessToken);
    }
}
