using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GalleryPixels.Api.Domain.Entities;
using GalleryPixels.Api.Domain.Extensions;
using GalleryPixels.Domain.Responses;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Login;

public class LoginUserCommandHandler(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IConfiguration configuration,
    ILogger<LoginUserCommandHandler> logger
) : IRequestHandler<LoginUserCommand, LoginUserCommandResult>
{
    public async ValueTask<LoginUserCommandResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
        if (user == null)
        {
            logger.LogDebug("Login attempt failed for non-existent email");
            return new LoginUserCommandResult(new LoginUserResponse(null), null);
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            logger.LogDebug("Login attempt failed for email");
            return new LoginUserCommandResult(new LoginUserResponse(null), null);
        }

        logger.LogDebug("Login attempt succeeded");
        var token = GenerateJwtToken(user);
        return new LoginUserCommandResult(new LoginUserResponse(token), user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim> { new(JwtRegisteredClaimNames.Sub, user.Id), new(JwtRegisteredClaimNames.Email, user.Email ?? throw new NullReferenceException("Email can never be null!")) };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetJwtKey()));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(7);

        var token = new JwtSecurityToken(
            issuer: configuration.GetJwtIssuer(),
            audience: configuration.GetJwtAudience(),
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}