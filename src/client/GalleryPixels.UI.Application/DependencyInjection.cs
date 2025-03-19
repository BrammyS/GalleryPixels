using System.Text.Json;
using GalleryPixels.UI.Application.Providers;
using GalleryPixels.UI.Application.Services;
using GalleryPixels.UI.Domain;
using GalleryPixels.UI.Domain.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryPixels.UI.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddAuthorizationCore();
        services.AddScoped<IImagePlaceHolderService, ImagePlaceHolderService>();
        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
        
        return services.RegisterDomain();
    }
}