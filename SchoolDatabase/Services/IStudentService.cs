using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Model.Entity.User;

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
        public Task AssignToCourse(CourseSubscribeDTO dto);
    }
}
