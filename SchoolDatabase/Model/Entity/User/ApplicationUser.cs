using Microsoft.AspNetCore.Identity;

namespace SchoolDatabase.Model.Entity.User
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string NeptunId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
