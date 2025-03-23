using GalleryPixels.UI.Application.Services;
using Microsoft.JSInterop;

namespace GalleryPixels.UI.Infrastructure.Services;

public class DarkModeService(IJSRuntime jsRuntime) : IDarkModeService
{
    public async Task UseDarkMode()
    {
        await jsRuntime.InvokeVoidAsync("useDarkMode").ConfigureAwait(false);
    }

    public async Task UseLightMode()
    {
        await jsRuntime.InvokeVoidAsync("useLightMode").ConfigureAwait(false);
    }

    public async Task RespectOsPreference()
    {
        await jsRuntime.InvokeVoidAsync("respectOsPreference").ConfigureAwait(false);
    }

    public async Task<bool> IsDarkMode()
    {
        return await jsRuntime.InvokeAsync<bool>("isDarkMode").ConfigureAwait(false);
    }
}