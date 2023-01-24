using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services;

namespace SchoolDatabase.UnitOfWork
{
    public interface IStudentUnitOfWork
    {
        Task AssignToCourse(CourseSubscribeDTO dto);
        IQueryable<Course> GetAllByStudentAndSemester(int id, int semesterId, bool containDeleted);
    }
}
