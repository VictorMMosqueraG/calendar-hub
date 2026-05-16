namespace Application.Features.Calendar.GetEvents.Interfaces;

using Application.Features.Calendar.GetEvents.Queries;

public interface ICalendarRequestBuilder
{
    (string Url, string AccessToken)? Build(GetEventsQuery query);
}
