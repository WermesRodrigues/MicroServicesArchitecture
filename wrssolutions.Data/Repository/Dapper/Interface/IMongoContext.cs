using wrssolutions.Domain.MongoEntities.LoggerMongo;
using MongoDB.Driver;

namespace wrssolutions.Data.Repository.Dapper.Interface
{
    public interface IMongoContext
    {
        IMongoCollection<LoggerMongo> LoggerMongo { get; }
    }
}
