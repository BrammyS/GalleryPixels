using Mediator;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Login;

public record LoginUserCommand(string Email, string Password) : IRequest<LoginUserResponse>;