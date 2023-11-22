
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using wrssolutions.Domain.Entities;

namespace wrssolutions.Data.Mapping
{
    public class mpPerson : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            builder.ToTable("Person");

            //Chave Primaria
            builder.HasKey(c => c.PersonID);


            builder.HasOne(e => e.ClientCompany).WithMany(e => e.People).HasForeignKey(e => e.CompanyID);

            #region Default Values

            #endregion
        }
    }
}
