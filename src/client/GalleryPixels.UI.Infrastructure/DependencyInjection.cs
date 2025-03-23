using Blazored.LocalStorage;
using GalleryPixels.UI.Application.Services;
using GalleryPixels.UI.Domain.Extensions;
using GalleryPixels.UI.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ILocalStorageService = GalleryPixels.UI.Application.Services.ILocalStorageService;

namespace GalleryPixels.UI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(
            InfrastructureConstants.GalleryPixelsApiClientName,
            client =>
            {
                client.BaseAddress = new Uri(configuration.GetGalleryPixelsApiUrl(), UriKind.Absolute);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }
        );
        
        services.AddBlazoredLocalStorage();
        services.AddScoped<IMagicGrid, MagicGrid>();
        services.AddScoped<ILocalStorageService, LocalStorageService>();
        services.AddScoped<IGalleryPixelsApiService, GalleryPixelsApiService>();
        services.AddSingleton<IToastService, ToastService>();


        return services;
    }
}