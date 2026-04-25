namespace Application.Features.OAuth.GetAuthUrl.Dtos;

public record GetAuthUrlResponseDto
{
    public string? Url { get; init; }
}
