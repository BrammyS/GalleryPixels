using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Register;

public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger, UserManager<IdentityUser> userManager) : IRequestHandler<RegisterUserCommand, RegisterUserResponse?>
{
    public async ValueTask<RegisterUserResponse?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new IdentityUser { UserName = request.Username, Email = request.Email };
        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return null;
        }

        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Failed to register user: {Username}, {Errors}", request.Username, result.Errors.Select(x => $"{x.Code}: {x.Description}"));
        }

        return new RegisterUserResponse(result.Errors.Select(error => new RegisterUserErrorDetails(error.Code, error.Description)).ToList());
    }
}