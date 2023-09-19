using Microsoft.JSInterop;

namespace GalleryPixels.UI.Masonry;

public class MasonryGrid : IMasonryGrid
{
    private readonly IJSRuntime _jsRuntime;

    public MasonryGrid(IJSRuntime jSRuntime)
    {
        _jsRuntime = jSRuntime;
    }

    public ValueTask InitAsync(
        string containerSelector,
        int gutterSize,
        int items
    )
    {
        return _jsRuntime.InvokeVoidAsync(
            "initMasonry",
            containerSelector,
            gutterSize,
            items
        );
    }
}