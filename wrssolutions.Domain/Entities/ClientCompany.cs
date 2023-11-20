using wrssolutions.Domain.Commom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wrssolutions.Domain.Entities
{
    public class ClientCompany : EntityBase
    {
        [Key]
        public int CompanyID { get; set; }
        public required string CompanyName { get; set; }
    }
}
