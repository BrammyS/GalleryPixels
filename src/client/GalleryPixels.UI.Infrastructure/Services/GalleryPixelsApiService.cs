using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;
using GalleryPixels.UI.Application;
using GalleryPixels.UI.Application.Builders;
using GalleryPixels.UI.Application.Services;
using Microsoft.Extensions.Logging;

namespace GalleryPixels.UI.Infrastructure.Services;

public class GalleryPixelsApiService(
    ILogger<GalleryPixelsApiService> logger,
    IHttpClientFactory httpClientFactory,
    ILocalStorageService localStorageService,
    JsonSerializerOptions serializerOptions
) : IGalleryPixelsApiService
{
    /// <inheritdoc />
    public async Task<TEntity?> GetAsync<TEntity>(string endpoint, IEnumerable<KeyValuePair<string, string>>? queries = null, CancellationToken ct = default) where TEntity : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithQueryParameters(queries)
            .WithMethod(HttpMethod.Get);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity?> PostAsync<TEntity, TBody>(string endpoint, TBody body, CancellationToken ct = default)
        where TEntity : notnull where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Post)
            .WithBody(body, serializerOptions);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task PostAsync<TBody>(string endpoint, TBody body, CancellationToken ct = default) where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Post)
            .WithBody(body, serializerOptions);

        await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity?> PostAsync<TEntity>(string endpoint, CancellationToken ct = default) where TEntity : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Post);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity?> PatchAsync<TEntity, TBody>(string endpoint, TBody body, CancellationToken ct = default)
        where TEntity : notnull where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Patch)
            .WithBody(body, serializerOptions);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task PatchAsync<TBody>(string endpoint, TBody body, CancellationToken ct = default) where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Patch)
            .WithBody(body, serializerOptions);

        await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity?> DeleteAsync<TEntity>(string endpoint, IEnumerable<KeyValuePair<string, string>>? queries = null, CancellationToken ct = default) where TEntity : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithQueryParameters(queries)
            .WithMethod(HttpMethod.Delete);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string endpoint, IEnumerable<KeyValuePair<string, string>>? queries = null, CancellationToken ct = default)
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithQueryParameters(queries)
            .WithMethod(HttpMethod.Delete);

        await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<TEntity?> PutAsync<TEntity, TBody>(string endpoint, TBody body, CancellationToken ct = default)
        where TEntity : notnull where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Put)
            .WithBody(body, serializerOptions);

        return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task PutAsync<TBody>(string endpoint, TBody body, CancellationToken ct = default) where TBody : notnull
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Put)
            .WithBody(body, serializerOptions);

        await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task PutAsync(string endpoint, CancellationToken ct = default)
    {
        var requestBuilder = new HttpRequestMessageBuilder(endpoint)
            .WithMethod(HttpMethod.Put);

        await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
    }

    private async Task<TEntity?> SendRequestAsync<TEntity>(HttpRequestMessageBuilder requestBuilder, CancellationToken ct) where TEntity : notnull
    {
        var httpClient = await GetClientAsync();
        using var request = requestBuilder.Build();
        using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);

        return await DeserializeResponseAsync<TEntity>(response, ct).ConfigureAwait(false);
    }

    private async Task SendRequestAsync(HttpRequestMessageBuilder requestBuilder, CancellationToken ct)
    {
        var httpClient = httpClientFactory.CreateClient("Discord");
        using var request = requestBuilder.Build();
        using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            logger.LogWarning("Unsuccessful request: {StatusCode} {ReasonPhrase}", response.StatusCode, response.ReasonPhrase);
        }
    }

    private async Task<TEntity?> DeserializeResponseAsync<TEntity>(HttpResponseMessage response, CancellationToken ct = default) where TEntity : notnull
    {
        if (response.IsSuccessStatusCode)
        {
            var entity = await JsonSerializer.DeserializeAsync<TEntity>(await response.Content.ReadAsStreamAsync(ct).ConfigureAwait(false), serializerOptions, ct).ConfigureAwait(false);
            return entity is not null ? entity : throw new NoNullAllowedException("TEntity can not be null");
        }

        logger.LogWarning("Unsuccessful request: {StatusCode} {ReasonPhrase}", response.StatusCode, response.ReasonPhrase);
        return default;
    }

    private async Task<HttpClient> GetClientAsync()
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