using GalleryPixels.Api.Application;
using GalleryPixels.Api.Infrastructure;

namespace GalleryPixels.Api;

public static class DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection RegisterApi(this IServiceCollection services)
    {
        return services
            .RegisterApplication()
            .RegisterInfrastructure();
    }
}