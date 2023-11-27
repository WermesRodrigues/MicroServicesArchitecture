using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using wrssolutions.Domain.Entities;

namespace wrssolutions.Data.Mapping
{
    public class mpOrder : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.ToTable("Order");

            //Chave Primaria
            builder.HasKey(c => c.Id);

            #region Default Values

            #endregion
        }
    }
}
