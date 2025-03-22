namespace GalleryPixels.UI.Infrastructure.Services;

public interface IMagicGrid
{
    ValueTask InitAsync(
        string containerSelector,
        int gutterSize,
        int items
    );
}