using GalleryPixels.UI.Application.Services;

namespace GalleryPixels.UI.Infrastructure.Services;

public class LocalStorageService(Blazored.LocalStorage.ILocalStorageService storageService) : ILocalStorageService
{
    public ValueTask<string?> GetStringAsync(string key)
    {
        return storageService.GetItemAsStringAsync(key);
    }

    public ValueTask SetStringAsync(string key, string value)
    {
        return storageService.SetItemAsStringAsync(key, value);
    }

    public ValueTask RemoveAsync(string key)
    {
        return storageService.RemoveItemAsync(key);
    }
}