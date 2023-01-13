using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services;

namespace SchoolDatabase.UnitOfWork
{
    public interface IStudentUnitOfWork
    {
        IQueryable<Course> GetAllByStudentAndSemester(int id, int semesterId, bool containDeleted);
    }
}
