using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.UnitOfWork.Interface
{
    public interface ICourseUnitOfWork
    {
        public Task<Course> GetCourseById(int id);
        public IQueryable<Course> GetCourseFilteredByTime(DateTime from, DateTime to);
    }
}
