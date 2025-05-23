﻿using System.Reflection;
using GalleryPixels.Api.Application.Endpoints.Version;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GalleryPixels.Api.Controllers;

[AllowAnonymous]
[Route("api/version")]
[Route("api/v{apiVersion}/version")]
public class VersionController(IMediator mediator) : ApiController(mediator)
{
    /// <summary>
    ///     Shows the current version and the build date of the API.
    /// </summary>
    /// <remarks>
    ///     GET: /api/version
    /// </remarks>
    /// <returns>
    ///     The requested <see cref="VersionResponse" />.
    /// </returns>
    /// <response code="200">Returned when the request was successful.</response>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<VersionResponse>> GetVersion()
    {
        var query = new GetVersionQuery(Assembly.GetExecutingAssembly());
        var result = await Mediator.Send(query).ConfigureAwait(false);
        return Ok(result);
    }
}