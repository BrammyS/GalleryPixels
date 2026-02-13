using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GalleryPixels.Api.Infrastructure.Persistence;

public class GalleryPixelsDbContext(DbContextOptions<GalleryPixelsDbContext> context) : IdentityDbContext(context)
{
    public async Task ExecuteMigrationAsync()
    {
        await Database.MigrateAsync().ConfigureAwait(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GalleryPixelsDbContext).Assembly);
    }
}