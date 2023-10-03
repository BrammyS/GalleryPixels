using GalleryPixels.Api.Application.Persistence.UnitOfWorks;
using GalleryPixels.Api.Infrastructure.Persistence.Configurations;
using GalleryPixels.Api.Infrastructure.Persistence.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryPixels.Api.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        services.AddSingleton<MongoContext>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();

        services.Scan(
            scan => scan
                .FromAssemblyOf<ICollectionConfigurator>()
                .AddClasses(classes => classes.AssignableTo<ICollectionConfigurator>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );

        return services;
    }
}