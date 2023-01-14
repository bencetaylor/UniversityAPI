using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.UnitOfWork
{
    public class CourseUnitOfWork : UnitOfWork, ICourseUnitOfWork
    {
        public CourseUnitOfWork(SchoolAPIDbContext context) : base(context)
        {
        }

        public IQueryable<Course> GetCourseFilteredByTime(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
