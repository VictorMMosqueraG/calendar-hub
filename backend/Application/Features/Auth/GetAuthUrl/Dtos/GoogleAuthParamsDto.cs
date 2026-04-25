namespace Application.Features.Auth.GetAuthUrl.Dtos;

public class GoogleAuthParamsDto
{
    public string? ClientId { get; init; }
    public string? RedirectUri { get; init; }
    public string ResponseType { get; init; } = "code";
    public string? Scope { get; init; }
    public string AccessType { get; init; } = "offline";
    public string Prompt { get; init; } = "consent";
}
