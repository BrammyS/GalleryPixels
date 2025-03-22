using GalleryPixels.Domain.Requests;
using GalleryPixels.Domain.Responses;
using Mediator;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Login;

public record LoginUserCommand(string Email, string Password) : LoginUserRequest(Email, Password), IRequest<LoginUserResponse>;