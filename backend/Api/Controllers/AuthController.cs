namespace Api.Controllers;

using Api.Attributes;
using Application.Features.Auth.Disconnect.Commands;
using Application.Features.Auth.GetStatus.Dtos;
using Application.Features.Auth.GetStatus.Queries;
using Asp.Versioning;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Manages authentication status and provider connections.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Gets the authentication status for all providers.
    /// </summary>
    [HttpGet("status")]
    [ProducesResponseType(typeof(ResultDto<GetStatusResponseDto>), StatusCodes.Status200OK)]
    public async Task<ResultDto<GetStatusResponseDto>> GetStatus(
        CancellationToken cancellationToken
    ) => await mediator.Send(new GetStatusQuery(), cancellationToken);

    /// <summary>
    /// Disconnects the specified provider.
    /// </summary>
    [HttpDelete("{provider}")]
    [RouteParameterMapping("provider", "Provider")]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultDto> Disconnect(
        DisconnectCommand command,
        CancellationToken cancellationToken
    ) => await mediator.Send(command, cancellationToken);
}
