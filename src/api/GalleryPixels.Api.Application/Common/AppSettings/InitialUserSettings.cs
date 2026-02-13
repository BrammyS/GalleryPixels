namespace GalleryPixels.Api.Application.Common.AppSettings;

public class InitialUserSettings()
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public const string Position = "InitialUserSettings";
}