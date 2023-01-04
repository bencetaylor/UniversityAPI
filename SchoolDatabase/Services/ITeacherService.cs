using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public interface ITeacherService
    {
        public IQueryable<Teacher> GetAll();
        public IQueryable<Subject> GetSubjectsByTeacherAndSemester(int TeacherId, int SemesterId);
    }
}
