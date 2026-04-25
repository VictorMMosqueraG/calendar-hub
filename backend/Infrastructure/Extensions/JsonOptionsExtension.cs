namespace Infrastructure.Extensions;

using System.Text.Json;

public static class JsonOptionsExtension
{
    public static readonly JsonSerializerOptions SnakeCaseOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };
}
