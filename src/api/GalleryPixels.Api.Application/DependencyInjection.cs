using System.Reflection;
using FluentValidation;
using GalleryPixels.Api.Application.Common.Pipelines;
using GalleryPixels.Api.Domain;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryPixels.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.RegisterDomain();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediator(
            options =>
            {
                options.Namespace = null;
                options.ServiceLifetime = ServiceLifetime.Transient;
            }
        );
        
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehaviour<,>)); // 1st
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>)); // 2nd

        return services;
    }
}