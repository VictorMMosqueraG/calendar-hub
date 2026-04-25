namespace Application.Features.Auth.GetAuthUrl.Dtos;

public record GetAuthUrlResponseDto
{
    public string? Url { get; init; }
}
