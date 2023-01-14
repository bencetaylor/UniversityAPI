using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public interface IStudentService
    {
        public IQueryable<Student> GetStudents(bool containDeleted);
        public Student GetStudent(int StudentId);
        public Task UpdateStudent(Student Student);
        public Task CreateStudent(Student Student);
        public Task DeleteStudentById(int id);
        public IQueryable<Course> GetAllByStudentAndSemester(int id, int semesterId, bool containDeleted);
    }
}
