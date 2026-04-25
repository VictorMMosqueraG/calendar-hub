namespace Api.Controllers;

using Api.Attributes;
using Application.Features.Auth.GetAuthUrl.Queries;
using Asp.Versioning;
using Core.Dtos.ResponsesDto;
using Core.Ports.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/auth")]
public class AuthController(IMediator mediator, IOAuthService oAuthService, ITokenStore tokenStore) : ControllerBase
{
    [HttpGet("{provider}")]
    [RouteParameterMapping("provider", nameof(GetAuthUrlQuery.Provider))]
    public async Task<ResultDto> Login(
        GetAuthUrlQuery query,
        CancellationToken cancellationToken
    ) => await mediator.Send(query, cancellationToken);

    [HttpGet("{provider}/callback")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Callback(
        string provider,
        [FromQuery] string code,
        CancellationToken cancellationToken)
    {
        var accessToken = await oAuthService.ExchangeCodeForTokenAsync(provider, code, cancellationToken);
        tokenStore.SetToken(provider, accessToken);

        return Redirect("http://localhost:4200");
    }

    [HttpGet("status")]
    public IActionResult Status()
        => Ok(tokenStore.GetConnectedProviders());

    [HttpDelete("{provider}")]
    public IActionResult Disconnect(string provider)
    {
        tokenStore.RemoveToken(provider);
        return Ok(new { provider, connected = false });
    }
}
