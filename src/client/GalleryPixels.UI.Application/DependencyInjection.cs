using GalleryPixels.UI.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryPixels.UI.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        return services.RegisterDomain();
    }
}