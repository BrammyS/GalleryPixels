using BlazorPro.BlazorSize;
using GalleryPixels.UI.Application;
using GalleryPixels.UI.Infrastructure;

namespace GalleryPixels.UI;

public static class DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection RegisterUI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IResizeListener, ResizeListener>();
        
        return services
            .RegisterApplication()
            .RegisterInfrastructure(configuration);
    }
}