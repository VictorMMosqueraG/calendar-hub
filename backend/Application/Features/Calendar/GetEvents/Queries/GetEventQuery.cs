namespace Application.Features.Calendar.GetEvents.Queries;

using Application.Features.Calendar.GetEvents.Dtos;
using Core.Dtos.ResponsesDto;
using MediatR;

public record GetEventsQuery : IRequest<ResultDto<List<GetEventsResponseDto>>>
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
}
