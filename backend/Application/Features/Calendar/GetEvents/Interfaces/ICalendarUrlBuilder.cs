namespace Application.Features.Calendar.GetEvents.Interfaces;

using Application.Features.Calendar.GetEvents.Queries;

public interface ICalendarUrlBuilder
{
    string Build(GetEventsQuery query);
}
