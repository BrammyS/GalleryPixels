using GalleryPixels.Api.Application.Endpoints.Auth.Login;
using GalleryPixels.Api.Application.Endpoints.Auth.Register;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GalleryPixels.Api.Controllers;

[AllowAnonymous]
public class AuthController(IMediator mediator) : ApiController(mediator)
{
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
        var response = await Mediator.Send(command).ConfigureAwait(false);
        if (response.IsSuccess) return Ok(response);

        return Unauthorized();
    }
}