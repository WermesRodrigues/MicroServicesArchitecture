using wrssolutions.Domain.MongoEntities.LoggerMongo;

namespace wrssolutions.Services.Interfaces
{
    public interface ISvcLoggerMongo
    {
        Task<List<LoggerMongo>> getAllEmpreendimentos();
        void Insert(LoggerMongo loggerMongo);
        void Update(LoggerMongo loggerMongo);
        void Disable(LoggerMongo loggerMongo);
    }
}
