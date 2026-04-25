namespace Api.Controllers;

using Asp.Versioning;
using Core.Ports.Auth;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/auth")]
public class AuthController(IOAuthService oAuthService, ITokenStore tokenStore) : ControllerBase
{
    [HttpGet("{provider}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Login(string provider)
    {
        var url = oAuthService.GetAuthorizationUrl(provider);
        return Redirect(url);
    }

    [HttpGet("{provider}/callback")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Callback(
        string provider,
        [FromQuery] string code,
        CancellationToken cancellationToken)
    {
        var accessToken = await oAuthService.ExchangeCodeForTokenAsync(provider, code, cancellationToken);
        tokenStore.SetToken(provider, accessToken);

        return Ok(new { provider, connected = true });
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
