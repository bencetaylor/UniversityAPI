using Microsoft.AspNetCore.Identity;

namespace SchoolDatabase.Model.Entity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public string NeptunId { get; set; }
        public string Department { get; set; }

        //public bool IsRailwayWorker { get; set; }
        //public string RailwayCompanyName { get; set; }
    }
}
