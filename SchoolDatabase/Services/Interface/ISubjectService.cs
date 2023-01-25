using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services.Interface
{
    public interface ISubjectService
    {
        public IQueryable<Subject> GetSubjects(bool containDeleted);
        public Task<Subject> GetSubject(int id);
        public Task CreateSubject(Subject subject);
        public Task UpdateSubject(Subject subject);
        public Task DeleteSubject(int id);
    }
}
