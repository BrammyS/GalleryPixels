namespace GalleryPixels.Api.Application.Persistence.Repositories;

/// <summary>
///     This is a generic Repository that should be used in all table specific Repositories.
/// </summary>
/// <typeparam name="T">The object type of the table/collection</typeparam>
public interface IRepository<T> where T : class
{

}