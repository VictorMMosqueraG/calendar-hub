namespace Api.Controllers;

using Application.Features.Calendar.GetEvents.Dtos;
using Application.Features.Calendar.GetEvents.Queries;
using Asp.Versioning;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Manages calendar events across connected providers.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/calendars")]
[Produces("application/json")]
public class CalendarController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Gets calendar events within a date range.
    /// </summary>
    [HttpGet("events")]
    [ProducesResponseType(typeof(ResultDto<List<GetEventsResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultDto<List<GetEventsResponseDto>>> GetEvents(
        [FromQuery] GetEventsQuery query,
        CancellationToken cancellationToken
    ) => await mediator.Send(query, cancellationToken);
}
