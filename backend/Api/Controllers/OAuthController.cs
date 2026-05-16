namespace Api.Controllers;

using Api.Attributes;
using Application.Features.OAuth.ExchangeToken.Commands;
using Application.Features.OAuth.GetAuthUrl.Dtos;
using Application.Features.OAuth.GetAuthUrl.Queries;
using Asp.Versioning;
using Core.Dtos.ResponsesDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Handles OAuth authentication flow with external providers.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/oauth")]
[Produces("application/json")]
public class OAuthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Gets the authentication URL for the specified provider.
    /// </summary>
    [HttpGet("{provider}/auth-url")]
    [RouteParameterMapping("provider", "Provider")]
    [ProducesResponseType(typeof(ResultDto<GetAuthUrlResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultDto<GetAuthUrlResponseDto>> GetAuthUrl(
        GetAuthUrlQuery query,
        CancellationToken cancellationToken
    ) => await mediator.Send(query, cancellationToken);

    /// <summary>
    /// Exchanges the authorization code for a token.
    /// </summary>
    [HttpPost("{provider}/callback")]
    [RouteParameterMapping("provider", "Provider")]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultDto> Callback(
        [FromBody] ExchangeTokenCommand command,
        CancellationToken cancellationToken
    ) => await mediator.Send(command, cancellationToken);
}
