using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using wrssolutions.Domain.Entities;

namespace wrssolutions.Data.Mapping
{    
    public class mpClientCompany : IEntityTypeConfiguration<ClientCompany>
    {
        public void Configure(EntityTypeBuilder<ClientCompany> builder)
        {

            builder.ToTable("ClientCompany");

            //Chave Primaria
            builder.HasKey(c => c.CompanyID);

            #region Default Values

            #endregion
        }
    }
}
