using System.Reflection;
using System.Text;
using FluentValidation;
using GalleryPixels.Api.Application.Common.Pipelines;
using GalleryPixels.Api.Domain;
using GalleryPixels.Api.Domain.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GalleryPixels.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDomain();
        services.AddAuthorization();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediator(
            options =>
            {
                options.Namespace = null;
                options.ServiceLifetime = ServiceLifetime.Transient;
            }
        );

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehaviour<,>)); // 1st
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>)); // 2nd

        services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(
                options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.GetJwtIssuer(),
                        ValidAudience = configuration.GetJwtAudience(),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetJwtKey()))
                    };
                }
            );

        services.Configure<IdentityOptions>(
            options =>
            {
                options.User.AllowedUserNameCharacters = configuration.GetAllowedUserNameCharacters();
                options.User.RequireUniqueEmail = configuration.GetRequiredUniqueUserEmail();
                options.Password.RequireDigit = configuration.GetRequirePasswordDigit();
                options.Password.RequireLowercase = configuration.GetRequirePasswordLowercase();
                options.Password.RequireUppercase = configuration.GetRequirePasswordUppercase();
                options.Password.RequireNonAlphanumeric = configuration.GetRequirePasswordNonAlphanumeric();
                options.Password.RequiredLength = configuration.GetRequiredPasswordLength();
            }
        );

        return services;
    }
}