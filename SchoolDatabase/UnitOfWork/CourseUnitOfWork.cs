using Microsoft.EntityFrameworkCore;
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
            return GetRepository<Course>().GetAll()
                .Include(c => c.Subject)
                .Include(c => c.Semester)
                .Where(c => from < c.Semester.EndDate || to < c.Semester.StartDate)
                .IgnoreQueryFilters().AsQueryable();
        }
    }
}
