namespace Application.Features.Calendar.GetEvents.Extensions;

using System.Web;
using Application.Features.Calendar.GetEvents.Dtos;

public static class GoogleCalendarQueryParamsExtensions
{
    //FIX: handle a better way
    public static string ToUrl(this GoogleCalendarQueryParamsDto queryParams, string baseUrl)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["timeMin"] = queryParams.TimeMin.ToString("o");
        query["timeMax"] = queryParams.TimeMax.ToString("o");
        query["singleEvents"] = queryParams.SingleEvents.ToString().ToLower();
        query["orderBy"] = queryParams.OrderBy;

        return $"{baseUrl}?{query}";
    }
}
