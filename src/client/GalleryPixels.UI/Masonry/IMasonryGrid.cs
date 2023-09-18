namespace GalleryPixels.UI.Masonry;

public interface IMasonryGrid
{
    ValueTask Init(
        string containerSelector,
        string itemSelector,
        int gutterSize,
        float transitionDurationSecs = .5f
    );
}