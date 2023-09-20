namespace GalleryPixels.UI.Masonry;

public interface IMagicGrid
{
    ValueTask InitAsync(
        string containerSelector,
        int gutterSize,
        int items
    );
}