namespace Infrastructure.Helpers;

using System.Text.Json;

public static class JsonOptionsHelper
{
    public static readonly JsonSerializerOptions SnakeCaseOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };
}
