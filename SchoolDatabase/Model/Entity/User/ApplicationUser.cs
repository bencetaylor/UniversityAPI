using Microsoft.AspNetCore.Identity;
using SchoolDatabase.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SchoolDatabase.Model.Entity.User
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [NeptunCodeValidationAttribute]
        public string NeptunId { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required, MaxLength(100)]
        public string Department { get; set; }
    }
}
