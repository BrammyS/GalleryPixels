using System.Security.Claims;

namespace GalleryPixels.UI.Domain.Services;

public interface IJwtService
{
    ClaimsPrincipal Deserialize(string jwtToken);
}