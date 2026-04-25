namespace Application.Features.OAuth.GetAuthUrl.Dtos;

using System.Web;

public record GoogleAuthParamsDto
{
    public string? ClientId { get; init; }
    public string? RedirectUri { get; init; }
    public string ResponseType { get; init; } = "code";
    public string? Scope { get; init; }
    public string AccessType { get; init; } = "offline";
    public string Prompt { get; init; } = "consent";

    public string ToUrl(string authUrl)
        => $"{authUrl}" +
            $"?client_id={HttpUtility.UrlEncode(ClientId)}" +
            $"&redirect_uri={HttpUtility.UrlEncode(RedirectUri)}" +
            $"&response_type={ResponseType}" +
            $"&scope={HttpUtility.UrlEncode(Scope)}" +
            $"&access_type={AccessType}" +
            $"&prompt={Prompt}";
}
