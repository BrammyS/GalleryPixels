using GalleryPixels.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryPixels.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        return services.RegisterDomain();
    }
}