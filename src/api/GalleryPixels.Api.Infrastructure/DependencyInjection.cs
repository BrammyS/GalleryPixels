﻿using GalleryPixels.Api.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryPixels.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddMongoDb();
        return services;
    }
}