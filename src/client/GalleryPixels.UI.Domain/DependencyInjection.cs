using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryPixels.UI.Domain;

public static class DependencyInjection
{
    public static IServiceCollection RegisterDomain(this IServiceCollection services)
    {
        services.AddSingleton(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = false });
        return services;
    }
}