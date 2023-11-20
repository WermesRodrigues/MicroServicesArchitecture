using wrssolutions.Data.Entity;
using wrssolutions.Data.Repository.Dapper.Interface;
using wrssolutions.Domain.Commom;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace wrssolutions.Data.Repository.Entity
{
    public class EFRepository<T> : IEFRepository<T> where T : EntityBase
    {
        private readonly EFContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public EFRepository(EFContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<T> GetAll(int take)
        {
            return entities.Take(take).AsEnumerable();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return entities.Where(predicate).ToList();
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var result = entities.Add(entity);
            context.SaveChanges();

            return result.Entity;
        }

        public void InsertRange(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.AddRange(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    this.Entities.Remove(entity);

                this.context.SaveChanges();
            }
            catch
            {

            }
        }

        public virtual void DeleteRange(Func<T, bool> Expression)
        {
            try
            {
                if (Expression == null)
                    throw new ArgumentNullException("entities");

                var dlt = this.Table.Where(Expression).AsEnumerable().ToList();
                if (dlt != null)
                {
                    this.Entities.RemoveRange(dlt);
                    this.context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (entities == null)
                    entities = context.Set<T>();
                return entities;
            }
        }
    }
}
