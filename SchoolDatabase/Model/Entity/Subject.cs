using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolDatabase.Model.Entity
{
    public class Subject : AbstractEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Code { get; set; }

        [Required]
        public int Credit { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }

    public class SubjectEntityTypeConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            //throw new NotImplementedException();
        }
    }
}
