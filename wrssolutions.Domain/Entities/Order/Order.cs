
using System.ComponentModel.DataAnnotations;
using wrssolutions.Domain.Commom;

namespace wrssolutions.Domain.Entities
{
    public class Order : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
