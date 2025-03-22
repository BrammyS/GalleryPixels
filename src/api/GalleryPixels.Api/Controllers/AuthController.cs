using GalleryPixels.Api.Application.Endpoints.Auth.Login;
using GalleryPixels.Api.Application.Endpoints.Auth.Register;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GalleryPixels.Api.Controllers;

[AllowAnonymous]
public class AuthController(IMediator mediator) : ApiController(mediator)
{
    private const string RefreshTokenCookieKey = "refreshToken";

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var response = await Mediator.Send(command).ConfigureAwait(false);
        if (response == null) return Ok();

        return BadRequest(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await Mediator.Send(command).ConfigureAwait(false);
        if (!result.Response.IsSuccess)
        {
            return Unauthorized();
        }

        AppendRefreshTokenCookie(result.User!, HttpContext.Response.Cookies);
        return Ok(result.Response);
    }

    private static void AppendRefreshTokenCookie(IdentityUser user, IResponseCookies cookies)
    {
        var options = new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddMinutes(60) };
        cookies.Append(RefreshTokenCookieKey, user.SecurityStamp!, options);
    }
}