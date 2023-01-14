using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.UnitOfWork
{
    public interface ICourseUnitOfWork
    {
        public IQueryable<Course> GetCourseFilteredByTime(DateTime from, DateTime to);
    }
}
