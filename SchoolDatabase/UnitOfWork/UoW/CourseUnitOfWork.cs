using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.UnitOfWork.Interface;

namespace SchoolDatabase.UnitOfWork.UoW
{
    public class CourseUnitOfWork : UnitOfWork, ICourseUnitOfWork
    {
        public CourseUnitOfWork(SchoolAPIDbContext context) : base(context)
        {
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await GetDbSet<Course>()
                .Include(c => c.Subject)
                .Include(c => c.Semester)
                .Include(c => c.Students)
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(e => e.Id == id);
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
