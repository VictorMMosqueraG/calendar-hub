namespace Infrastructure.Wrappers;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Interfaces.Wrappers;
using Core.Exceptions;
using Infrastructure.Extensions;
using System.Net.Http.Headers;
using System.Text.Json;

public class GoogleCalendarWrapper(HttpClient httpClient) : ICalendarProvider
{
    public async Task<GoogleCalendarResponseDto?> GetEventsAsync(
        string url,
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        using var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new BadRequestException($"Google Calendar API error ({response.StatusCode}): {errorBody}");
        }

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<GoogleCalendarResponseDto>(json, JsonOptionsExtension.SnakeCaseOptions);
    }
}
