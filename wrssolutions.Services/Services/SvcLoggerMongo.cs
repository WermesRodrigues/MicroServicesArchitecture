using wrssolutions.Data.Repository.Dapper.Interface;
using wrssolutions.Domain.MongoEntities.LoggerMongo;
using wrssolutions.Services.Interfaces;
using MongoDB.Driver;

namespace wrssolutions.Services.Services
{
    public class SvcLoggerMongo : ISvcLoggerMongo
    {
        private readonly IMongoContext _context;

        public SvcLoggerMongo(IMongoContext context)
        {
            _context = context;
        }

        public async void Insert(LoggerMongo loggerMongo)
        {
            await _context.LoggerMongo.InsertOneAsync(loggerMongo);
        }

        public async void Update(LoggerMongo loggerMongo)
        {
            var filter = Builders<LoggerMongo>.Filter.Eq(s => s.Id, loggerMongo.Id);
            var update = Builders<LoggerMongo>.Update.Set(s => s.Error, loggerMongo.Error);

            var result = await _context.LoggerMongo.UpdateOneAsync(filter, update);
        }

        public async void Disable(LoggerMongo loggerMongo)
        {
            var filter = Builders<LoggerMongo>.Filter.Eq(s => s.Id, loggerMongo.Id);
            var update = Builders<LoggerMongo>.Update.Set(s => s._del, false);

            var result = await _context.LoggerMongo.UpdateOneAsync(filter, update);
        }

        public async Task<List<LoggerMongo>> getAllEmpreendimentos()
        {
            return (List<LoggerMongo>)await _context.LoggerMongo.AsQueryable().ToListAsync();
        }
    }
}
