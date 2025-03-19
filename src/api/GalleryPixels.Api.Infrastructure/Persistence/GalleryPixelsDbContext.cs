using Microsoft.EntityFrameworkCore;

namespace GalleryPixels.Api.Infrastructure.Persistence;

public class GalleryPixelsDbContext(DbContextOptions<GalleryPixelsDbContext> context) : DbContext(context)
{
    public async Task ExecuteMigrationAsync()
    {
        await Database.MigrateAsync();
    }
}