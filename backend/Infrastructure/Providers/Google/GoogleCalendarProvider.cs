namespace Infrastructure.Providers.Google;

using Core.Dtos.AppSettingDto;
using Core.Entities;
using Core.Ports.Providers;
using global::Microsoft.Extensions.Options;
using Infrastructure.Providers.Base;

public class GoogleCalendarProvider(
    HttpClient httpClient,
    IOptions<CalendarSettingsDto> calendarSettings,
    ICalendarTokenAccessor tokenAccessor
) : CalendarProviderBase(httpClient, tokenAccessor)
{
    public override string ProviderName => "Google";

    private readonly CalendarSettingsDto _settings = calendarSettings.Value;

    protected override string BuildUrl(DateTime from, DateTime to)
        => $"{_settings.GoogleBaseUrl}?timeMin={from.ToUniversalTime():yyyy-MM-ddTHH:mm:ssZ}&timeMax={to.ToUniversalTime():yyyy-MM-ddTHH:mm:ssZ}&singleEvents=true&orderBy=startTime";

    protected override async Task<IReadOnlyList<CalendarEvent>> MapResponseAsync(
        HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var result = await DeserializeAsync<GoogleCalendarEventResponse>(response, cancellationToken);

        if (result?.Items is null)
            return [];

        return result.Items.Select(item => new CalendarEvent
        {
            Id          = item.Id,
            Title       = item.Summary ?? "(Sin título)",
            Start       = ParseDateTimeSafe(item.Start.DateTime ?? item.Start.Date),
            End         = ParseDateTimeSafe(item.End.DateTime ?? item.End.Date),
            IsAllDay    = item.Start.Date is not null,
            Provider    = ProviderName,
            Account     = item.Organizer?.Email,
            Location    = item.Location,
            Description = item.Description
        }).ToList();
    }
}
