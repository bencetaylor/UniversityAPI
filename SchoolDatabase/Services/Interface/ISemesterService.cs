using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services.Interface
{
    public interface ISemesterService
    {
        public IQueryable<Semester> GetSemesters(bool containDeleted);
        public Task<Semester> GetSemester(int id);
        public Task CreateSemester(Semester semester);
        public Task UpdateSemester(Semester semester);
        public Task DeleteSemester(int id);
    }
}
