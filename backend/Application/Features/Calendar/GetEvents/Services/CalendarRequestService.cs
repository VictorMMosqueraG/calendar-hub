namespace Application.Features.Calendar.GetEvents.Services;

using Application.Features.Calendar.GetEvents.Queries;
using Application.Features.Calendar.GetEvents.Interfaces;
using Application.Interfaces.Services;
using Core.Constants;

public class CalendarRequestService(
    ITokenStore tokenStore,
    ICalendarUrlBuilder calendarUrlBuilder
) : ICalendarRequestBuilder
{
    private readonly ITokenStore _tokenStore = tokenStore;
    private readonly ICalendarUrlBuilder _calendarUrlBuilder = calendarUrlBuilder;

    public (string Url, string AccessToken)? Build(GetEventsQuery query)
    {
        var accessToken = _tokenStore.GetToken(ProviderConstant.Google);

        if (accessToken is null or "")
            return null;

        var url = _calendarUrlBuilder.Build(query);

        return (url, accessToken);
    }
}
