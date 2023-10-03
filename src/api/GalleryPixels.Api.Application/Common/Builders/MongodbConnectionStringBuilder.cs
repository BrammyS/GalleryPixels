using System.Text;
using GalleryPixels.Api.Domain.Extensions;
using Microsoft.Extensions.Configuration;

namespace GalleryPixels.Api.Application.Common.Builders;

public class MongodbConnectionStringBuilder
{
    private const bool IsMongoDbAtlas = false;
    private static string? _host;
    private static string? _port;
    private static string? _database;
    private static string? _atuhDatabase;
    private static string? _username;
    private static string? _password;

    public MongodbConnectionStringBuilder FromConfiguration(IConfiguration configuration)
    {
        _host = configuration.GetMongodbHost();
        _port = configuration.GetMongodbPort();
        _database = configuration.GetMongodbDatabase();
        _atuhDatabase = configuration.GetMongodbAuthDatabase();
        _username = configuration.GetMongodbUsername();
        _password = configuration.GetMongodbPassword();
        
        return this;
    }

    public string Build()
    {
        if (string.IsNullOrWhiteSpace(_host)) throw new ArgumentNullException(_host);
        if (string.IsNullOrWhiteSpace(_database)) throw new ArgumentNullException(_database);
        if (string.IsNullOrWhiteSpace(_atuhDatabase)) throw new ArgumentNullException(_atuhDatabase);
        if (!IsMongoDbAtlas && string.IsNullOrWhiteSpace(_port)) throw new ArgumentNullException(_port);

        var csBuilder = new StringBuilder();
        // ReSharper disable once HeuristicUnreachableCode
        csBuilder.Append($"{(IsMongoDbAtlas ? "mongodb+srv" : "mongodb")}://");

        if (!string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password))
            csBuilder.Append($"{_username}:{_password}@");

        csBuilder.Append($"{_host}");

        if (!IsMongoDbAtlas)
            csBuilder.Append($":{_port}");

        csBuilder.Append($"/{_atuhDatabase}");
        csBuilder.Append("?retryWrites=true&w=majority&compressors=zlib,zstd&maxPoolSize=1024");

        return csBuilder.ToString();
    }
}