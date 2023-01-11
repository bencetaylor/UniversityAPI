using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDatabase.Model.Entity
{
    public class Course : AbstractEntity
    {
        [Required]
        public int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        [Required]
        public int SemesterId { get; set; }

        [ForeignKey("SemesterId")]
        public Semester Semester { get; set; }

        [Required]
        public String Timetable { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }

    public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            //throw new NotImplementedException();
        }
    }
}
