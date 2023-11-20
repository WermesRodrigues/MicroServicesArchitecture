using wrssolutions.Domain.Entities.Person;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace wrssolutions.Data.Mapping
{
    public class mpPerson : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            builder.ToTable("Person");

            //Chave Primaria
            builder.HasKey(c => c.PersonID);

            #region Default Values

            #endregion
        }
    }
}
