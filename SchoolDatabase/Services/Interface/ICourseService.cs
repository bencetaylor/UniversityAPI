using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services.Interface
{
    public interface ICourseService
    {
        public Task<Course> GetCourse(int id);
        public IQueryable<Course> GetCourses(bool containDeleted);
        public Task CreateCourse(Course course);
        public Task UpdateCourse(Course course);
        public Task DeleteCourse(int id);
        public IQueryable<Course> GetCourseFilteredByTime(DateTime from, DateTime to);
    }
}
