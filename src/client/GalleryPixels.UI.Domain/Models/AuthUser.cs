using System.Security.Claims;

namespace GalleryPixels.UI.Domain.Models;

public class AuthUser
{
    public required string Token { get; set; }
    public required ClaimsPrincipal ClaimsPrincipal { get; set; }
}