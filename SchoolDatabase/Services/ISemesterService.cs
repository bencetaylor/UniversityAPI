using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public interface ISemesterService
    {
        public IQueryable<Semester> GetSemesters(bool containDeleted);
        public Semester GetSemester(int id);
        public Task CreateSemester(Semester semester);
        public Task UpdateSemester(Semester semester);
        public Task DeleteSemester(int id);
    }
}
