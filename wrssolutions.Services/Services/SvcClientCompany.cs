

using Microsoft.EntityFrameworkCore;
using wrssolutions.Data.Entity;
using wrssolutions.Data.Repository.Dapper.Interface;
using wrssolutions.Data.Repository.Entity;
using wrssolutions.Domain.Entities;
using wrssolutions.Domain.MongoEntities.LoggerMongo;
using wrssolutions.Services.Interfaces;

namespace wrssolutions.Services.Services
{
    public class SvcClientCompany : ISvcClientCompany
    {
        private IEFRepository<ClientCompany> model;
        private IDppRepository repository;
        private EFContext _objContext;
        private readonly ISvcLoggerMongo _svcLoggerMongo;

        public SvcClientCompany(
                                EFContext context,
                                IDppRepository _repository,
                                ISvcLoggerMongo svcLoggerMongo)
        {
            model = new EFRepository<ClientCompany>(context);
            repository = _repository;
            _objContext = context;
            _svcLoggerMongo = svcLoggerMongo;
        }


        public bool Insert(ClientCompany clientCompany)
        {
            try
            {
                model.Insert(clientCompany);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex?.Message!;
                //lOG
                _svcLoggerMongo.Insert(new LoggerMongo()
                {
                    Error = error
                });

                return false;
            }
        }

        public bool Update(ClientCompany clientCompany)
        {
            try
            {
                model.Update(clientCompany);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex?.Message!;

                //lOG
                _svcLoggerMongo.Insert(new LoggerMongo()
                {
                    Error = error
                });

                return false;
            }
        }


        public List<ClientCompany> GetCompanyWithPeople(int companyID)
        {
            try
            {
                return model.Table.Where(x => x.CompanyID == companyID).Include(y => y.People).ToList();
            }
            catch (Exception ex)
            {
                string error = ex?.Message!;

                //lOG
                _svcLoggerMongo.Insert(new LoggerMongo()
                {
                    Error = error
                });

                return null!;
            }
        }
    }
}
