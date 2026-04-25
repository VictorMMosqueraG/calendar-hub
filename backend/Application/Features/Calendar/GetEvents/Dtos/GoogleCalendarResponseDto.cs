namespace Application.Features.Calendar.GetEvents.Dtos;

public record GoogleCalendarResponseDto
{
    public IReadOnlyList<GoogleCalendarEventDto>? Items { get; init; }
}
