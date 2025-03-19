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
        var registered = await Mediator.Send(command).ConfigureAwait(false);
        if(registered == null) return Ok();
        
        return BadRequest(registered);
    } 
}