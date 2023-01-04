using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public class StudentService
    {
        private readonly SchoolAPIDbContext _context;

        public StudentService(SchoolAPIDbContext context)
        {
            _context = context;
        }

        public IQueryable<Student> GetStudents()
        {
            return _context.Set<Student>();
        }
    }
}
