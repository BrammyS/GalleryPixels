﻿using Microsoft.Extensions.Configuration;

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

    public static string GetMongodbHost(this IConfiguration configuration)
    {
        const string key = "Mongodb:Host";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetMongodbPort(this IConfiguration configuration)
    {
        const string key = "Mongodb:Port";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetMongodbDatabase(this IConfiguration configuration)
    {
        const string key = "Mongodb:Database";
        return GetAndValidateKey(configuration, key);
    }

    public static string GetMongodbAuthDatabase(this IConfiguration configuration)
    {
        const string key = "Mongodb:AuthDatabase";
        return GetAndValidateKey(configuration, key);
    }

    public static string? GetMongodbUsername(this IConfiguration configuration)
    {
        const string key = "Mongodb:Username";
        return GetKey(configuration, key);
    }

    public static string? GetMongodbPassword(this IConfiguration configuration)
    {
        const string key = "Mongodb:Password";
        return GetKey(configuration, key);
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