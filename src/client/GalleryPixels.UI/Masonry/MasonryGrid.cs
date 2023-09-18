using Microsoft.JSInterop;

namespace GalleryPixels.UI.Masonry;

public class MasonryGrid : IMasonryGrid
{
    private readonly IJSRuntime _jsRuntime;

    public MasonryGrid(IJSRuntime jSRuntime)
    {
        _jsRuntime = jSRuntime;
    }

    public ValueTask Init(
        string containerSelector,
        string itemSelector,
        int gutterSize,
        float transitionDurationSecs = .5F
    )
    {
        var transitionDurationStr = $"{transitionDurationSecs}s";

        return _jsRuntime.InvokeVoidAsync(
            "initMasonry",
            containerSelector,
            itemSelector,
            gutterSize,
            transitionDurationStr
        );
    }
}