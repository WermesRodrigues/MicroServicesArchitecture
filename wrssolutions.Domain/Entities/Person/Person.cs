using wrssolutions.Domain.Commom;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace wrssolutions.Domain.Entities
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
        /// <summary>
        /// ClientCompany
        /// </summary>
        [JsonIgnore]
        public virtual ClientCompany ? ClientCompany { get; set; }
    }
}
