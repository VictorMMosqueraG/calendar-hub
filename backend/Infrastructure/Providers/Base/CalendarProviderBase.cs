namespace Infrastructure.Providers.Base;

using Core.Entities;
using Core.Exceptions;
using Core.Ports.Providers;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public abstract class CalendarProviderBase(
    HttpClient httpClient,
    ICalendarTokenAccessor tokenAccessor
) : ICalendarProvider
{
    public abstract string ProviderName { get; }

    public bool IsAvailable => !string.IsNullOrEmpty(tokenAccessor.GetToken(ProviderName));

    public async Task<IReadOnlyList<CalendarEvent>> GetEventsAsync(
        DateTime          from,
        DateTime          to,
        CancellationToken cancellationToken = default)
    {
        var accessToken = tokenAccessor.GetToken(ProviderName);

        using var request = new HttpRequestMessage(HttpMethod.Get, BuildUrl(from, to));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        ConfigureHeaders(request);

        using var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new BadRequestException($"{ProviderName} API error ({response.StatusCode}): {errorBody}");
        }

        return await MapResponseAsync(response, cancellationToken);
    }

    protected abstract string BuildUrl(DateTime from, DateTime to);

    protected virtual void ConfigureHeaders(HttpRequestMessage request) { }

    protected abstract Task<IReadOnlyList<CalendarEvent>> MapResponseAsync(
        HttpResponseMessage response, CancellationToken cancellationToken);

    protected async Task<T?> DeserializeAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
        => await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);

    protected static DateTime ParseDateTimeSafe(string? value)
        => DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result)
            ? result
            : DateTime.MinValue;
}
