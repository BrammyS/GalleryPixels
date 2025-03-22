using BlazorPro.BlazorSize;
using GalleryPixels.UI.Application;
using GalleryPixels.UI.Infrastructure;
using GalleryPixels.UI.Infrastructure.Services;
using GalleryPixels.UI.Services;

namespace GalleryPixels.UI;

public static class DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection RegisterUI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMagicGrid, MagicGrid>();
        services.AddScoped<IResizeListener, ResizeListener>();
        services.AddSingleton<ToastService>();
        
        return services
            .RegisterApplication()
            .RegisterInfrastructure(configuration);
    }
}