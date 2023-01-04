using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public interface ISemesterService
    {
        public IQueryable<Semester> GetSemesters();
    }
}
