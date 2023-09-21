using GalleryPixels.UI.Application.Services;
using GalleryPixels.UI.Domain;
using GalleryPixels.UI.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryPixels.UI.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddScoped<IImagePlaceHolderService, ImagePlaceHolderService>();

        return services.RegisterDomain();
    }
}