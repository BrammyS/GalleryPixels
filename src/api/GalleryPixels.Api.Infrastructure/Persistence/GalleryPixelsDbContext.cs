using Microsoft.EntityFrameworkCore;

namespace GalleryPixels.Api.Infrastructure.Persistence;

public class GalleryPixelsDbContext : DbContext
{
    public async Task ExecuteMigrationAsync()
    {
        await Database.MigrateAsync();
    }
}