using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Model.Entity.User;

namespace SchoolDatabase.UnitOfWork
{
    public class StudentUnitOfWork : UnitOfWork, IStudentUnitOfWork
    {
        public StudentUnitOfWork(SchoolAPIDbContext context) : base(context)
        {

        }

        public IQueryable<Course> GetAllByStudentAndSemester(int id, int semesterId, bool containDeleted)
        {
            Student student;

            if (containDeleted)
            {
                student = GetRepository<Student>().GetAll()
                .Include(s => s.Courses)
                .ThenInclude(c => c.Subject)
                .IgnoreQueryFilters()
                .FirstOrDefault(t => t.Id == id);
                if (student == null) return null;
            }
            else
            {
                student = GetRepository<Student>().GetAll()
                .Include(s => s.Courses)
                .ThenInclude(c => c.Subject)
                .FirstOrDefault(t => t.Id == id);
            }

            if (student == null) throw new Exception("There is no student for this id " + id + "!"); ;
            return student.Courses.Where(c => c.SemesterId == semesterId).AsQueryable();
        }

        public async Task AssignToCourse(CourseSubscribeDTO dto)
        {
            var student = GetDbSet<Student>().FirstOrDefault(e => e.Id == dto.UserId);
            var course = GetDbSet<Course>()
                .Include(c => c.Students)
                .FirstOrDefault(e => e.Id == dto.CourseId);
            if (student != null && course != null)
            {
                course.Students.Add(student);
                GetDbSet<Course>().Update(course);
            }
        }
    }
}
