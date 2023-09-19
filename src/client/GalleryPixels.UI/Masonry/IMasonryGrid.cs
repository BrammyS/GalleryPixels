namespace GalleryPixels.UI.Masonry;

public interface IMasonryGrid
{
    ValueTask InitAsync(
        string containerSelector,
        int gutterSize,
        int items
    );
}