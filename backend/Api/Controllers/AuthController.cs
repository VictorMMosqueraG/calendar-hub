namespace Api.Controllers;

using Api.Attributes;
using Application.Features.Auth.Disconnect.Commands;
using Application.Features.Auth.GetStatus.Dtos;
using Application.Features.Auth.GetStatus.Queries;
using Asp.Versioning;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpGet("status")]
    public async Task<ResultDto<GetStatusResponseDto>> Status(
        CancellationToken cancellationToken
    ) => await mediator.Send(new GetStatusQuery(), cancellationToken);

    [HttpDelete("{provider}")]
    [RouteParameterMapping("provider", "Provider")]
    public async Task<ResultDto> Disconnect(
        DisconnectCommand command,
        CancellationToken cancellationToken
    ) => await mediator.Send(command, cancellationToken);
}
