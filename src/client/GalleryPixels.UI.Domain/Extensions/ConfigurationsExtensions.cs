using Microsoft.Extensions.Configuration;

namespace GalleryPixels.UI.Domain.Extensions;

public static class ConfigurationsExtensions
{
    public static string GetGalleryPixelsApiUrl(this IConfiguration configuration)
    {
        const string key = "Api:Url";
        return GetAndValidateKey(configuration, key);
    }

    private static string GetAndValidateKey(IConfiguration configuration, string key)
    {
        return GetKey(configuration, key) ?? throw new ArgumentNullException(key, $"{key} is not set in the configuration file.");
    }

    private static string? GetKey(IConfiguration configuration, string key)
    {
        return configuration[key];
    }
}