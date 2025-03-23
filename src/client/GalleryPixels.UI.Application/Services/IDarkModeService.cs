namespace GalleryPixels.UI.Application.Services;

public interface IDarkModeService
{
    Task UseDarkMode();
    Task UseLightMode();
    Task RespectOsPreference();
    Task<bool> IsDarkMode();
}