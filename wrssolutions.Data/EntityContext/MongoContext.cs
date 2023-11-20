
using wrssolutions.Data.Repository.Dapper.Interface;
using wrssolutions.Domain.MongoEntities.LoggerMongo;
using MongoDB.Driver;

namespace wrssolutions.Data.EntityContext
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _db;
        public MongoContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
        }
        public IMongoCollection<LoggerMongo> LoggerMongo => _db.GetCollection<LoggerMongo>("LoggerMongo");
    }
}
