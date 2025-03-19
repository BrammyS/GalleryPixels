using Microsoft.Extensions.Configuration;

namespace GalleryPixels.Api.Domain.Extensions;

public static class ConfigurationsExtensions
{
    public static string GetS3AccessKey(this IConfiguration configuration)
    {
        const string key = "S3:AccessKey";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetS3SecretKey(this IConfiguration configuration)
    {
        const string key = "S3:SecretKey";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetS3EndpointUrl(this IConfiguration configuration)
    {
        const string key = "S3:EndpointUrl";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetS3BucketName(this IConfiguration configuration)
    {
        const string key = "S3:BucketName";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetS3PublicUrl(this IConfiguration configuration)
    {
        const string key = "S3:PublicUrl";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetPostgresConnectionString(this IConfiguration configuration)
    {
        const string key = "ConnectionStrings:Postgres";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetJwtIssuer(this IConfiguration configuration)
    {
        const string key = "Jwt:ValidIssuer";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetJwtAudience(this IConfiguration configuration)
    {
        const string key = "Jwt:ValidAudience";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetJwtKey(this IConfiguration configuration)
    {
        const string key = "Jwt:Key";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetAllowedUserNameCharacters(this IConfiguration configuration)
    {
        const string key = "Identity:AllowedUserNameCharacters";
        return GetAndValidateKey(configuration, key);
    }

    public static bool GetRequiredUniqueUserEmail(this IConfiguration configuration)
    {
        const string key = "Identity:RequireUniqueEmail";
        return bool.Parse(GetAndValidateKey(configuration, key));
    }

    public static bool GetRequirePasswordDigit(this IConfiguration configuration)
    {
        const string key = "Identity:Password:RequireDigit";
        return bool.Parse(GetAndValidateKey(configuration, key));
    }

    public static bool GetRequirePasswordLowercase(this IConfiguration configuration)
    {
        const string key = "Identity:Password:RequireLowercase";
        return bool.Parse(GetAndValidateKey(configuration, key));
    }

    public static bool GetRequirePasswordUppercase(this IConfiguration configuration)
    {
        const string key = "Identity:Password:RequireUppercase";
        return bool.Parse(GetAndValidateKey(configuration, key));
    }

    public static bool GetRequirePasswordNonAlphanumeric(this IConfiguration configuration)
    {
        const string key = "Identity:Password:RequireNonAlphanumeric";
        return bool.Parse(GetAndValidateKey(configuration, key));
    }

    public static int GetRequiredPasswordLength(this IConfiguration configuration)
    {
        const string key = "Identity:Password:RequiredLength";
        return int.Parse(GetAndValidateKey(configuration, key));
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