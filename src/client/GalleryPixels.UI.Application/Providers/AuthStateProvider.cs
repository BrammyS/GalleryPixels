namespace GalleryPixels.UI.Application.Providers;

using Microsoft.AspNetCore.Components.Authorization;
using GalleryPixels.UI.Domain.Providers;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Services;

public class AuthStateProvider(ILocalStorageService localStorageService) : AuthenticationStateProvider, IAuthStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorageService.GetStringAsync(ApplicationConstants.AuthTokenKey);
        var identity = string.IsNullOrEmpty(token)
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await localStorageService.SetStringAsync(ApplicationConstants.AuthTokenKey, token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task MarkUserAsLoggedOut()
    {
        await localStorageService.RemoveAsync(ApplicationConstants.AuthTokenKey);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var payload = JsonSerializer.Deserialize<Dictionary<string, object>>(Encoding.UTF8.GetString(Convert.FromBase64String(token.Split('.')[1])));
        if (payload is null)
        {
            throw new InvalidOperationException("Invalid token");
        }

        return payload.Select(x => new Claim(x.Key, x.Value.ToString()!));
    }
}