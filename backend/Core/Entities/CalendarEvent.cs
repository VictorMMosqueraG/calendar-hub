namespace Core.Entities;

public class CalendarEvent
{
    public string? Id { get; init; }
    public string? Title { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public string? Provider { get; init; }
    public string? Account { get; init; }
    public string? Location { get; init; }
    public string? Description { get; init; }
    public bool IsAllDay { get; init; }
}
