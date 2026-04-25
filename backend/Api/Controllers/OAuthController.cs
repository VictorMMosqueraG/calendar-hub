namespace Api.Controllers;

using Api.Attributes;
using Application.Features.Auth.ExchangeToken.Commands;
using Application.Features.Auth.GetAuthUrl.Dtos;
using Application.Features.Auth.GetAuthUrl.Queries;
using Asp.Versioning;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/oauth")]
public class OAuthController(IMediator mediator) : ControllerBase
{
    [HttpGet("{provider}")]
    [RouteParameterMapping("provider", "Provider")]
    public async Task<ResultDto<GetAuthUrlResponseDto>> Login(
        GetAuthUrlQuery query,
        CancellationToken cancellationToken
    ) => await mediator.Send(query, cancellationToken);

    [HttpPost("{provider}/callback")]
    [RouteParameterMapping("provider", "Provider")]
    public async Task<ResultDto> Callback(
        ExchangeTokenCommand command,
        CancellationToken cancellationToken
    ) => await mediator.Send(command, cancellationToken);
}
