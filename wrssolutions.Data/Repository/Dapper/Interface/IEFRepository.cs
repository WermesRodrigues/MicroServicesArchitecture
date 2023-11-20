using wrssolutions.Domain.Commom;
using System.Linq.Expressions;

namespace wrssolutions.Data.Repository.Dapper.Interface
{
    public interface IEFRepository<T> where T : EntityBase
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(int take);
        T Insert(T entity);
        void InsertRange(IEnumerable<T> entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void DeleteRange(Func<T, bool> Expression);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }
}
