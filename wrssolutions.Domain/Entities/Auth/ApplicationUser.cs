using Microsoft.AspNetCore.Identity;

namespace wrssolutions.Domain.Entities.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        //public string DataEventRecordsRole { get; set; }
        //public string SecuredFilesRole { get; set; }
    }
}
