using GalleryPixels.Api.Application;
using GalleryPixels.Api.Infrastructure;

namespace GalleryPixels.Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApi(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .RegisterApplication(configuration)
            .RegisterInfrastructure(configuration);
    }
}