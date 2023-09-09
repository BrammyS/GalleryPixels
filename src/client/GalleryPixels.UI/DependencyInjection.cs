using GalleryPixels.UI.Application;
using GalleryPixels.UI.Infrastructure;

namespace GalleryPixels.UI;

public static class DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection RegisterUI(this IServiceCollection services)
    {
        return services
            .RegisterApplication()
            .RegisterInfrastructure();
    }
}