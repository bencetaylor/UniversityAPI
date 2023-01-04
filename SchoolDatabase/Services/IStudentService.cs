using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public interface IStudentService
    {
        public IQueryable<Student> GetStudents();
    }
}
