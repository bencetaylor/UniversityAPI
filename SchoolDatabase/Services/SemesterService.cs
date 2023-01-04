using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly SchoolAPIDbContext _context;

        public SemesterService(SchoolAPIDbContext context)
        {
            _context = context;
        }

        public IQueryable<Semester> GetSemesters()
        {
            return _context.Set<Semester>();
        }
    }
}
