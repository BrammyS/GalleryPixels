using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GalleryPixels.Api.Domain.Extensions;
using GalleryPixels.Domain.Responses;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GalleryPixels.Api.Application.Endpoints.Auth.Login;

public class LoginUserCommandHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IConfiguration configuration
) : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    public async ValueTask<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
        if (user == null) return new LoginUserResponse(null);

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false).ConfigureAwait(false);
        if (!result.Succeeded) return new LoginUserResponse(null);

        var token = GenerateJwtToken(user);
        return new LoginUserResponse(token);
    }

    private string GenerateJwtToken(IdentityUser user)
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