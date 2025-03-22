using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;
using GalleryPixels.UI.Application;
using GalleryPixels.UI.Application.Services;

namespace GalleryPixels.UI.Infrastructure.Services;

public class GalleryPixelsApiService(
    IHttpClientFactory httpClientFactory,
    ILocalStorageService localStorageService,
    JsonSerializerOptions serializerOptions
) : IGalleryPixelsApiService
{
    public async Task<TEntity> DeserializeResponseAsync<TEntity>(HttpResponseMessage response, CancellationToken ct = default) where TEntity : notnull
    {
        var entity = await JsonSerializer.DeserializeAsync<TEntity>(await response.Content.ReadAsStreamAsync(ct).ConfigureAwait(false), serializerOptions, ct).ConfigureAwait(false);
        return entity is not null ? entity : throw new NoNullAllowedException("TEntity can not be null");
    }

    public async Task<HttpClient> GetClientAsync()
    {
        var client = httpClientFactory.CreateClient(InfrastructureConstants.GalleryPixelsApiClientName);

        var token = await localStorageService.GetStringAsync(ApplicationConstants.AuthTokenKey);
        if (token is not null)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return client;
    }
}