using GalleryPixels.Domain.Requests;
using Mediator;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Login;

public record LoginUserCommand(string Email, string Password) : LoginUserRequest(Email, Password), IRequest<LoginUserCommandResult>;