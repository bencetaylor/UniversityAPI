using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public interface ISemesterService
    {
        public IQueryable<Semester> GetSemesters(bool containDeleted);
        public IQueryable<Semester> GetSemesterById(int id);
        public Task CreateSemester(Semester semester);
        public Task UpdateSemester(Semester semester);
        public Task DeleteSemester(int id);
    }
}
