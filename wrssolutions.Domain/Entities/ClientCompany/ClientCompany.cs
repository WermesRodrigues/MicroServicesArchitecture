using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wrssolutions.Domain.Commom;

namespace wrssolutions.Domain.Entities
{
    public class ClientCompany : EntityBase
    {
        private List<Person>? _people;
        [Key]
        public int CompanyID { get; set; }
        public required string CompanyName { get; set; }

        [ForeignKey("CompanyID")]
        public virtual List<Person>? People
        {
            get { return _people ?? (_people = new List<Person>()); }
            set { _people = value; }
        }
    }
}
