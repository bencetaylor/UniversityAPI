using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolDatabase.Attributes;

namespace SchoolDatabase.Model.Entity
{
    public class Teacher : AbstractEntity
    {
        [Required]
        [NeptunCodeValidationAttribute]
        public string NeptunId { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public int PositionId { get; set; }

        [ForeignKey("PositionId")]
        public Position? Position { get; set; }

        public virtual ICollection<Course>? Courses { get; set; }
    }

    public class TeacherEntityTypeConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            //throw new NotImplementedException();
        }
    }
}
