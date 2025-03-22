using GalleryPixels.Domain.Requests;
using GalleryPixels.Domain.Responses.Register;
using Mediator;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Register;

public record RegisterUserCommand(string Username, string Email, string Password) : RegisterUserRequest(Username, Email, Password), IRequest<RegisterUserResponse>;
