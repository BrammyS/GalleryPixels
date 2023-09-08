using GalleryPixels.Application;
using GalleryPixels.Infrastructure;

namespace GalleryPixels.Web;

public static class DependencyInjection
{
    public static IServiceCollection RegisterWeb(this IServiceCollection services)
    {
        return services
            .RegisterApplication()
            .RegisterInfrastructure();
    }
}