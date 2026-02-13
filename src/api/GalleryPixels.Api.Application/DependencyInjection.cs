using System.Text;
using GalleryPixels.Api.Application.Common.AppSettings;
using GalleryPixels.Api.Application.Common.CustomAuthValidators;
using GalleryPixels.Api.Application.Common.Pipelines;
using GalleryPixels.Api.Application.Services;
using GalleryPixels.Api.Domain;
using GalleryPixels.Api.Domain.Entities;
using GalleryPixels.Api.Domain.Extensions;
using GalleryPixels.Api.Domain.Services;
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

        RegisterMediator(services);
        RegisterAuth(services, configuration);
        RegisterIdentityConfigurations(services, configuration);
        RegisterAppSettings(services, configuration);

        services.AddTransient<IUserValidator<User>, CustomUserValidator>();
        services.AddTransient<IInitialUserService, InitialUserService>();

        return services;
    }

    private static void RegisterMediator(IServiceCollection services)
    {
        services.AddMediator(options =>
            {
                options.Namespace = null;
                options.ServiceLifetime = ServiceLifetime.Transient;
            }
        );

        // Top to bottom is outer to inner.
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
    }

    private static void RegisterAuth(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(options =>
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
    }

    private static void RegisterIdentityConfigurations(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IdentityOptions>(options =>
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
    }

    private static void RegisterAppSettings(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<InitialUserSettings>(configuration.GetSection(InitialUserSettings.Position));
    }
}