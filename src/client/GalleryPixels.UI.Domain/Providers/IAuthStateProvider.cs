namespace GalleryPixels.UI.Domain.Providers;

public interface IAuthStateProvider
{
    Task MarkUserAsAuthenticated(string token);
    Task MarkUserAsLoggedOut();
}