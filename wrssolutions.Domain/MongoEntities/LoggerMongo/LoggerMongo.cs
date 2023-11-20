using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace wrssolutions.Domain.MongoEntities.LoggerMongo
{
    [BsonIgnoreExtraElements]
    public class LoggerMongo
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public required string Error { get; set; }
        public bool _del { get; set; }
    }
}
