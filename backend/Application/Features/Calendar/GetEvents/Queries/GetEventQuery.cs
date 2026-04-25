namespace Application.Features.Calendar.GetEvents.Queries;

using Application.Features.Calendar.GetEvents.Dtos;
using MediatR;

public record GetEventsQuery : IRequest<List<GetEventsResponseDto>>
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
}
