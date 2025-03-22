using GalleryPixels.Domain.Requests;
using GalleryPixels.Domain.Responses;
using GalleryPixels.UI.Application.Builders;
using GalleryPixels.UI.Application.Providers;
using GalleryPixels.UI.Domain.Services;
using Microsoft.AspNetCore.Components.Authorization;
using RegisterUserResponse = GalleryPixels.Domain.Responses.Register.RegisterUserResponse;

namespace GalleryPixels.UI.Application.Services;

public class AuthService(IGalleryPixelsApiService galleryPixelsApiService, AuthenticationStateProvider authStateProvider) : IAuthService
{
    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        var client = await galleryPixelsApiService.GetClientAsync();
        var request = new HttpRequestMessageBuilder("v1/auth/login")
            .WithMethod(HttpMethod.Post)
            .WithBody(new LoginUserRequest(email, password));

        var response = await client.SendAsync(request.Build(), HttpCompletionOption.ResponseContentRead);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var content = await galleryPixelsApiService.DeserializeResponseAsync<LoginUserResponse>(response);
        await CastedProvider.AuthenticatedAsync(content.Token!);
        return true;
    }

    public async Task<RegisterUserResponse> RegisterAsync(string username, string email, string password)
    {
        var client = await galleryPixelsApiService.GetClientAsync();
        var request = new HttpRequestMessageBuilder("v1/auth/register")
            .WithMethod(HttpMethod.Post)
            .WithBody(new RegisterUserRequest(username, email, password));

        var response = await client.SendAsync(request.Build(), HttpCompletionOption.ResponseContentRead);
        var content = await galleryPixelsApiService.DeserializeResponseAsync<RegisterUserResponse>(response);

        return content;
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