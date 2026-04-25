namespace Infrastructure.Providers.Google;

using System.Text.Json.Serialization;

public record GoogleCalendarEventResponse(
    [property: JsonPropertyName("items")] IReadOnlyList<GoogleCalendarEventItem> Items
);

public record GoogleCalendarEventItem(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("summary")] string? Summary,
    [property: JsonPropertyName("start")] GoogleCalendarEventTime Start,
    [property: JsonPropertyName("end")] GoogleCalendarEventTime End,
    [property: JsonPropertyName("organizer")] GoogleCalendarOrganizer? Organizer,
    [property: JsonPropertyName("location")] string? Location,
    [property: JsonPropertyName("description")] string? Description
);

public record GoogleCalendarEventTime(
    [property: JsonPropertyName("dateTime")] string? DateTime,
    [property: JsonPropertyName("date")] string? Date
);

public record GoogleCalendarOrganizer(
    [property: JsonPropertyName("email")] string? Email
);