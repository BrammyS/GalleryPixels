using GalleryPixels.UI.Application.Services;
using Microsoft.JSInterop;

namespace GalleryPixels.UI.Infrastructure.Services;

public class MagicGrid(IJSRuntime jSRuntime) : IMagicGrid
{
    public ValueTask InitAsync(
        string containerSelector,
        int gutterSize,
        int items
    )
    {
        return jSRuntime.InvokeVoidAsync(
            "initMasonry",
            containerSelector,
            gutterSize,
            items
        );
    }
}