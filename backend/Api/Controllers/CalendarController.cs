namespace Api.Controllers;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Queries;
using Asp.Versioning;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/calendar")]
public class CalendarController(IMediator mediator) : ControllerBase
{
    [HttpGet("events")]
    public async Task<ResultDto<List<GetEventsResponseDto>>> GetEvents(
        [FromQuery] GetEventsQuery query,
        CancellationToken cancellationToken
    ) => await mediator.Send(query, cancellationToken);
}
