using GalleryPixels.Domain.Requests;
using GalleryPixels.Domain.Responses;
using GalleryPixels.Domain.Responses.Register;
using GalleryPixels.UI.Application.Providers;
using GalleryPixels.UI.Domain.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace GalleryPixels.UI.Application.Services;

public class AuthService(IGalleryPixelsApiService galleryPixelsApiService, AuthenticationStateProvider authStateProvider) : IAuthService
{
    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        var result = await galleryPixelsApiService.PostAsync<LoginUserResponse, LoginUserRequest>("v1/auth/login", new LoginUserRequest(email, password));
        if (result is null || !result.IsSuccess)
        {
            return false;
        }

        await CastedProvider.AuthenticatedAsync(result.Token!);
        return true;
    }

    public async Task<IReadOnlyList<RegisterUserErrorDetails>?> RegisterAsync(string username, string email, string password)
    {
        var result = await galleryPixelsApiService.PostAsync<RegisterUserResponse, RegisterUserRequest>("v1/auth/register", new RegisterUserRequest(username, email, password));
        return result?.Errors;
    }

    public async Task LogoutAsync()
    { 
        await CastedProvider.LogoutAsync();
    }

    public async Task<bool> IsAuthenticated()
    {
        var token = await CastedProvider.GetTokenAsync();
        return token is not null;
    }
    
    private AuthStateProvider CastedProvider => (AuthStateProvider)authStateProvider;
}