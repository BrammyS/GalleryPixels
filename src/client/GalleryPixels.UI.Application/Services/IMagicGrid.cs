namespace GalleryPixels.UI.Application.Services;

public interface IMagicGrid
{
    ValueTask InitAsync(
        string containerSelector,
        int gutterSize,
        int items
    );
}