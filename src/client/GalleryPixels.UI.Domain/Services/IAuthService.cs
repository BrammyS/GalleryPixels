﻿using GalleryPixels.Domain.Responses.Register;

namespace GalleryPixels.UI.Domain.Services;

public interface IAuthService
{
    Task<bool> AuthenticateAsync(string email, string password);
    Task<RegisterUserResponse> RegisterAsync(string username, string email, string password);
    Task LogoutAsync();
    Task<bool> IsAuthenticated();
}