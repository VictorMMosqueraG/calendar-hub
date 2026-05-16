namespace Application.Features.Calendar.GetEvents.Dtos;

public record GoogleCalendarQueryParamsDto
{
    public DateTime TimeMin { get; init; }
    public DateTime TimeMax { get; init; }
    public bool SingleEvents { get; init; } = true;//FIX: not inicialized
    public string OrderBy { get; init; } = "startTime";//FIX: not inicialized
}
