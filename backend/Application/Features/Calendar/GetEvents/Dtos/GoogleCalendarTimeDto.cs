namespace Application.Features.Calendar.GetEvents.Dtos;

public record GoogleCalendarTimeDto
{
    public string? DateTime { get; init; }
    public string? Date { get; init; }
}
