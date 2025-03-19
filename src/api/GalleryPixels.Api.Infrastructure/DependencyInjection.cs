using GalleryPixels.Api.Domain.Extensions;
using GalleryPixels.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryPixels.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<GalleryPixelsDbContext>(opt => opt.UseNpgsql(configuration.GetPostgresConnectionString()));
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<GalleryPixelsDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}