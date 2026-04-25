namespace Application.Features.Auth.GetStatus.Dtos;

public record GetStatusResponseDto
{
    public Dictionary<string, bool>? Providers { get; init; }
}
