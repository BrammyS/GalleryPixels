using GalleryPixels.Api.Application.Common.Builders;
using GalleryPixels.Api.Domain.Extensions;
using GalleryPixels.Api.Infrastructure.Persistence.Configurations;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace GalleryPixels.Api.Infrastructure.Persistence;

/// <summary>
///     Hold the connection to the database.
/// </summary>
public class MongoContext : BaseMongoContext
{
    private readonly IMongoDatabase _database;

    /// <summary>
    ///     Creates a new <see cref="MongoContext" />.
    /// </summary>
    public MongoContext(
        IEnumerable<ICollectionConfigurator> collectionConfigurators,
        IConfiguration configuration
    ) : base(collectionConfigurators)
    {
        var connectionString = new MongodbConnectionStringBuilder()
            .FromConfiguration(configuration)
            .Build();

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(configuration.GetMongodbDatabase());
    }

    /// <summary>
    ///     Creates a new <see cref="MongoContext" />.
    /// </summary>
    public MongoContext(
        IEnumerable<ICollectionConfigurator> collectionConfigurators,
        string connectionString,
        string databaseName
    ) : base(collectionConfigurators)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    /// <summary>
    ///     Get a <see cref="IMongoCollection{TDocument}" />.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IMongoCollection{TDocument}" />.</typeparam>
    /// <param name="collectionName">The name of the <see cref="IMongoCollection{TDocument}" />.</param>
    /// <returns>Returns the requested <see cref="IMongoCollection{TDocument}" />.</returns>
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}