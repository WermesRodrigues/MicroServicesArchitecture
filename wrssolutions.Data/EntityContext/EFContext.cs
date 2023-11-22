using wrssolutions.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using wrssolutions.Domain.Entities;

namespace wrssolutions.Data.Entity
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            Database.SetCommandTimeout(180);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Mapping
            modelBuilder.Entity<Person>(new mpPerson().Configure);
            #endregion
        }

        #region Tables
        public DbSet<Person> Person { get; set; }
        #endregion
    }
}
