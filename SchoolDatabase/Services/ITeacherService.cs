using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Model.Entity.User;

namespace SchoolDatabase.Services
{
    public interface ITeacherService
    {
        public IQueryable<Teacher> GetTeachers(bool containDeleted);
        public Teacher GetTeacher(int id);
        public Task CreateTeacher(Teacher teacher);
        public Task UpdateTeacher(Teacher teacher);
        public Task DeleteTeacher(int id);
        public IQueryable<Course> GetAllByTeacherAndSemester(int id, int semesterId, bool containDeleted);
        public List<StudentDTO> GetAllStudentsByTeacherAndSemester(int id, int semesterId);
        public TeacherAggregateDTO GetTeacherAggregatedBySemester(int id, int semesterId);
        public Task AssignToCourse(CourseSubscribeDTO dto);
    }
}
