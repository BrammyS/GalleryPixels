﻿using GalleryPixels.UI.Application;
using GalleryPixels.UI.Infrastructure;
using GalleryPixels.UI.Masonry;

namespace GalleryPixels.UI;

public static class DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection RegisterUI(this IServiceCollection services)
    {
        services.AddScoped<IMasonryGrid, MasonryGrid>();
        
        return services
            .RegisterApplication()
            .RegisterInfrastructure();
    }
}