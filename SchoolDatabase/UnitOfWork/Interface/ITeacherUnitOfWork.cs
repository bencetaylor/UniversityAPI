using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.UnitOfWork.Interface
{
    public interface ITeacherUnitOfWork
    {
        public Task AssignToCourse(CourseSubscribeDTO dto);
        public IQueryable<Course> GetAllByTeacherAndSemester(int id, int semesterId, bool containDeleted);
        public List<StudentDTO> GetAllStudentsByTeacherAndSemester(int id, int semesterId);
        public TeacherAggregateDTO GetTeacherAggregatedBySemester(int id, int semesterId);
    }
}
