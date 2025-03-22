namespace GalleryPixels.UI.Domain.Providers;

public interface IAuthStateProvider
{
    Task AuthenticatedAsync(string token);
    Task LogoutAsync();
    ValueTask<string?> GetTokenAsync();
}