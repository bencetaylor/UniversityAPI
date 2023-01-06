using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public interface ICourseService
    {
        public IQueryable<Course> GetCourse(int id);
        public IQueryable<Course> GetCourses();
        public Task CreateCourse(Course course);
        public Task UpdateCourse(Course course);
        public Task DeleteCourse(int id);
    }
}
