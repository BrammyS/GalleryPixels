namespace GalleryPixels.UI.Application.Services;

public interface IGalleryPixelsApiService
{
    Task<TEntity> DeserializeResponseAsync<TEntity>(HttpResponseMessage response, CancellationToken ct = default) where TEntity : notnull;
    Task<HttpClient> GetClientAsync();
}