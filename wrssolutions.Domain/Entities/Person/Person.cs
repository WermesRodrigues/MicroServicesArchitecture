using wrssolutions.Domain.Commom;
using System.ComponentModel.DataAnnotations;

namespace wrssolutions.Domain.Entities.Person
{
    public class Person : EntityBase
    {
        [Key]
        public int PersonID { get; set; }
        public int CompanyID { get; set; }
        public required string Email { get; set; }
        public string? Fname { get; set; }
        public string? Mname { get; set; }
        public string? Lname { get; set; }
    }
}
