namespace GalleryPixels.UI.Application.Services;

public interface ILocalStorageService
{
    ValueTask<string?> GetStringAsync(string key);
    ValueTask SetStringAsync(string key, string value);
    ValueTask RemoveAsync(string key);
}