namespace Application.Features.Calendar.GetEvents.Dtos;

public record GoogleCalendarQueryParamsDto
{
    public DateTime TimeMin { get; init; }
    public DateTime TimeMax { get; init; }
    public bool SingleEvents { get; init; } = true;
    public string OrderBy { get; init; } = "startTime";

    public string ToUrl(string baseUrl)
        => $"{baseUrl}?timeMin={TimeMin:o}&timeMax={TimeMax:o}&singleEvents={SingleEvents.ToString().ToLower()}&orderBy={OrderBy}";
}
