using BlazorPro.BlazorSize;
using GalleryPixels.UI.Application;
using GalleryPixels.UI.Application.Services;
using GalleryPixels.UI.Infrastructure;
using GalleryPixels.UI.Services;

namespace GalleryPixels.UI;

public static class DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection RegisterUI(this IServiceCollection services)
    {
        services.AddScoped<IMagicGrid, MagicGrid>();
        services.AddScoped<IResizeListener, ResizeListener>();
        
        return services
            .RegisterApplication()
            .RegisterInfrastructure();
    }
}