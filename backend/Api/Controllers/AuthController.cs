namespace Api.Controllers;

using Asp.Versioning;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/auth")]
public class AuthController(ITokenStore tokenStore) : ControllerBase
{
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
