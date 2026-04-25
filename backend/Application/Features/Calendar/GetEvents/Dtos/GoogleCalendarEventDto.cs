namespace Application.Features.Calendar.GetEvents.Dtos;

public record GoogleCalendarEventDto
{
    public string? Id { get; init; }
    public string? Summary { get; init; }
    public GoogleCalendarTimeDto? Start { get; init; }
    public GoogleCalendarTimeDto? End { get; init; }
    public GoogleCalendarOrganizerDto? Organizer { get; init; }
    public string? Location { get; init; }
    public string? Description { get; init; }
}
