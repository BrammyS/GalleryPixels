using Mediator;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Register;

public record RegisterUserCommand(string Username, string Email, string Password) : IRequest<RegisterUserResponse?>;
