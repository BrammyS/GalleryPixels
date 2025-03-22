using System.Security.Claims;
using GalleryPixels.UI.Application.Services;
using GalleryPixels.UI.Domain.Models;
using GalleryPixels.UI.Domain.Providers;
using GalleryPixels.UI.Domain.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace GalleryPixels.UI.Application.Providers;

public class AuthStateProvider(
    ILocalStorageService localStorageService,
    IJwtService jwtService,
    ILogger<AuthStateProvider> logger
) : AuthenticationStateProvider, IAuthStateProvider
{
    private static readonly AuthenticationState NotAuthenticatedState = new(new ClaimsPrincipal());
    private AuthUser? _authUser;

    public async Task AuthenticatedAsync(string token)
    {
        await localStorageService.SetStringAsync(ApplicationConstants.AuthTokenKey, token);
        SetAuthUser(token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        _authUser = null;
        await localStorageService.RemoveAsync(ApplicationConstants.AuthTokenKey);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public ValueTask<string?> GetTokenAsync()
    {
        return localStorageService.GetStringAsync(ApplicationConstants.AuthTokenKey);
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorageService.GetStringAsync(ApplicationConstants.AuthTokenKey);
        if (token is not null && _authUser is null)
        {
            SetAuthUser(token);
        }

        return _authUser is not null ? new AuthenticationState(_authUser.ClaimsPrincipal) : NotAuthenticatedState;
    }

    private void SetAuthUser(string token)
    {
        var principal = jwtService.Deserialize(token);
        _authUser = new AuthUser { Token = token, ClaimsPrincipal = principal };
    }
}