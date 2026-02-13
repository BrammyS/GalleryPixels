using Microsoft.AspNetCore.Identity;

namespace GalleryPixels.Api.Domain.Entities;

public class User : IdentityUser
{
    public bool IsInitialUser { get; set; }
}