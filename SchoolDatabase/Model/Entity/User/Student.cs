using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolDatabase.Model.Entity
{
    public class Student : AbstractEntity
    {
        [Required, MinLength(6), MaxLength(6)]
        public string NeptunId { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Email { get; set; }

        public int SpecialityId { get; set; }

        [ForeignKey("SpecialityId")]
        public Speciality? Speciality { get; set; }

        public virtual ICollection<Course>? Courses { get; set; }
    }

    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //throw new NotImplementedException();
        }
    }
}
