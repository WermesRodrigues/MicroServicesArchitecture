using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wrssolutions.Domain.Entities;

namespace wrssolutions.Services.Interfaces
{
    public interface ISvcClientCompany
    {
        List<ClientCompany> GetCompanyWithPeople(int companyID);
        bool Insert(ClientCompany clientCompany);
        bool Update(ClientCompany clientCompany);
    }
}
