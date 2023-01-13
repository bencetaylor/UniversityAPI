using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

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

            if (student == null) return null;
            return student.Courses.Where(c => c.SemesterId == semesterId).AsQueryable();
        }
    }
}
