using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public interface IStudentService
    {
        public IQueryable<Student> GetStudents();
        public IQueryable<Student> GetStudent(int StudentId);
        Task UpdateStudent(Student Student);
        Task CreateStudent(Student Student);
        Task DeleteStudentById(int id);
    }
}
