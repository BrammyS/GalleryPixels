using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GalleryPixels.Api.Infrastructure.Persistence;

public class GalleryPixelsDbContext(DbContextOptions<GalleryPixelsDbContext> context) : IdentityDbContext<IdentityUser>(context)
{
    public async Task ExecuteMigrationAsync()
    {
        await Database.MigrateAsync().ConfigureAwait(false);
    }
}