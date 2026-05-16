namespace Application.Features.OAuth.GetAuthUrl.Extensions;

using System.Web;
using Application.Features.OAuth.GetAuthUrl.Dtos;

public static class GoogleAuthParamsExtensions
{
    public static string ToUrl(this GoogleAuthParamsDto dto, string authUrl)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["client_id"] = dto.ClientId ?? string.Empty;
        query["redirect_uri"] = dto.RedirectUri ?? string.Empty;
        query["response_type"] = dto.ResponseType ?? string.Empty;
        query["scope"] = dto.Scope ?? string.Empty;
        query["access_type"] = dto.AccessType ?? string.Empty;
        query["prompt"] = dto.Prompt ?? string.Empty;

        return $"{authUrl}?{query}";
    }
}
