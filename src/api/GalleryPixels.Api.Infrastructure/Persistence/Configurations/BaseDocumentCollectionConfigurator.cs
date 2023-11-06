using GalleryPixels.Api.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace GalleryPixels.Api.Infrastructure.Persistence.Configurations;

public class BaseDocumentCollectionConfigurator : ICollectionConfigurator
{
    /// <inheritdoc />
    public void ConfigureCollection()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(BaseDocument))) return;

        BsonClassMap.RegisterClassMap<BaseDocument>(
            cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.AddedAtUtc).SetElementName("AddedAtUtc").SetIsRequired(true);
                cm.MapIdProperty(c => c.BsonObjectId)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId))
                    .SetIsRequired(true);
            }
        );
    }
}