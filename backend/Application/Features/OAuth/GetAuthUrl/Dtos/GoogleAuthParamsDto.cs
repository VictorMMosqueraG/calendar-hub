namespace Application.Features.OAuth.GetAuthUrl.Dtos;

public record GoogleAuthParamsDto
{
    public string? ClientId { get; init; }
    public string? RedirectUri { get; init; }
    public string? ResponseType { get; init; }
    public string? Scope { get; init; }
    public string? AccessType { get; init; }
    public string? Prompt { get; init; }
}
