using wrssolutions.Data.Entity;
using wrssolutions.Data.Repository.Dapper.Interface;
using wrssolutions.Data.Repository.Entity;
using wrssolutions.Domain.Entities.Person;
using wrssolutions.Domain.MongoEntities.LoggerMongo;
using wrssolutions.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace wrssolutions.Services.Services
{
    public class SvcPerson : ISvcPerson
    {
        private IEFRepository<Person> model;
        private IDppRepository repository;
        private EFContext _objContext;
        private readonly ILogger _logger;
        private readonly ISvcLoggerMongo _svcLoggerMongo;

        public SvcPerson(
                                EFContext context,
                                IDppRepository _repository,
                                ILogger logger,
                                ISvcLoggerMongo svcLoggerMongo)
        {
            model = new EFRepository<Person>(context);
            repository = _repository;
            _objContext = context;
            _logger = logger;
            _svcLoggerMongo = svcLoggerMongo;
        }

        public Person? GetPersonByEmail(string email)
        {
            return model.Table.Where(x => x.Email == email).FirstOrDefault();
        }

        public bool Insert(Person person)
        {
            try
            {
                model.Insert(person);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex?.Message!;
                //lOG
                _logger.LogInformation(error);

                _svcLoggerMongo.Insert(new LoggerMongo()
                {
                    Error = error
                });

                return false;
            }
        }

        public bool Update(Person person)
        {
            try
            {
                model.Update(person);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex?.Message!;

                //lOG
                _logger.LogInformation(error);

                _svcLoggerMongo.Insert(new LoggerMongo()
                {
                    Error = error
                });

                return false;
            }
        }
    }
}
