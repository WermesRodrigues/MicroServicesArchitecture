using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wrssolutions.Data.Repository.Dapper.Interface
{
    public interface IDppRepository
    {
        string GetDatabaseName();
        bool UpdateGeneric(string sql);
        T QueryFirstOrDefault<T>(string sql);
        List<T> QueryList<T>(string sql);
        int QueryAsCount(string sql);
    }
}
