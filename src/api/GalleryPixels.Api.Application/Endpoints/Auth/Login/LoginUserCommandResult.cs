using GalleryPixels.Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Login;

public record LoginUserCommandResult(LoginUserResponse Response, IdentityUser? User);