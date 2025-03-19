using GalleryPixels.UI.Infrastructure.Services;
using Microsoft.JSInterop;

namespace GalleryPixels.UI.Services;

public class MagicGrid : IMagicGrid
{
    private readonly IJSRuntime _jsRuntime;

    public MagicGrid(IJSRuntime jSRuntime)
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