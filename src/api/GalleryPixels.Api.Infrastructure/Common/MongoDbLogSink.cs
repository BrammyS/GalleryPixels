using GalleryPixels.Api.Application.Common.Builders;
using GalleryPixels.Api.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;
using Serilog.Configuration;
using Serilog.Sinks.Mongodb.TimeSeries.Configurations;
using Serilog.Sinks.Mongodb.TimeSeries.Extensions;

namespace GalleryPixels.Api.Infrastructure.Common;

public static class MongoDbLogSink
{
    /// <summary>
    ///     Add a MongoDB log sink to serilog.
    ///     This will save all the logs to the database.
    /// </summary>
    /// <returns>
    ///     The sink configurations.
    /// </returns>
    public static LoggerConfiguration MongoDb(this LoggerSinkConfiguration sinkConfiguration, IConfiguration configuration)
    {
        var connectionString = new MongodbConnectionStringBuilder()
            .FromConfiguration(configuration)
            .Build();

        var client = new MongoClient(connectionString);
        var mongoDatabase = client.GetDatabase(configuration.GetMongodbDatabase());

        var configs = new MongoDbTimeSeriesSinkConfig(mongoDatabase)
        {
            CollectionName = "Logs",
            SyncingPeriod = TimeSpan.FromSeconds(10),
            EagerlyEmitFirstEvent = true,
            LogsExpireAfter = TimeSpan.FromDays(7)
        };

        return sinkConfiguration.MongoDbTimeSeriesSink(configs);
    }
}