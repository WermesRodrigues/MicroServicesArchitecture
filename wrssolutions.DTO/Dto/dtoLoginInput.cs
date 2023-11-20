using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wrssolutions.DTO.Dto
{
    public class dtoLoginInput
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool RememberLogin { get; set; }
    }
}
