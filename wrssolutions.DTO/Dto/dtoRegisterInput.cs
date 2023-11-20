using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wrssolutions.DTO.Dto
{
    public class dtoRegisterInput
    {
        public int CompanyID { get; set; }
        public required string Email { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
