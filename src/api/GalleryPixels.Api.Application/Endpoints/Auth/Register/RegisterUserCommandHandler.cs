using GalleryPixels.Api.Domain.Entities;
using GalleryPixels.Domain.Responses.Register;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Register;

public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger, UserManager<User> userManager) : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    public async ValueTask<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.Username,
            Email = request.Email,
            IsInitialUser = request.IsInitialUser
        };

        var result = await userManager.CreateAsync(user, request.Password).ConfigureAwait(false);
        if (result.Succeeded)
        {
            return new RegisterUserResponse([]);
        }

        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Failed to register user: {Username}, {Errors}", request.Username, result.Errors.Select(x => $"{x.Code}: {x.Description}"));
        }

        return new RegisterUserResponse(result.Errors.Select(error => new RegisterUserErrorDetails(error.Code, error.Description)).ToList());
    }
}