using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public class CourseService : ICourseService
    {
        private readonly SchoolAPIDbContext _context;

        public CourseService(SchoolAPIDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Course> GetCourse(int id)
        {
            return _context.Set<Course>().ToList().Where(e => e.Id == id).AsQueryable();
        }

        /// <summary>
        /// List all courses
        /// </summary>
        /// <returns></returns>
        public IQueryable<Course> GetCourses()
        {
            return _context.Set<Course>();
        }

        /// <summary>
        /// Create a course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task CreateCourse(Course course)
        {
            await _context.Set<Course>().AddAsync(course);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteCourse(int id)
        {
            var course = _context.Set<Course>().FirstOrDefault(e => e.Id == id);
            if(course != null)
            {
                course.Deleted = true;
                _context.Set<Course>().Update(course);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update a course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task UpdateCourse(Course course)
        {
            _context.Set<Course>().Update(course);
            await _context.SaveChangesAsync();
        }

        //IQueryable<Course> GetAllByTeacherAndSemester(int id, int semesterId)
        //{
            

        //    //return _context.Set<Teacher>()
        //    //    .Where(t => t.Id == id)
        //    //    .Include(t => t.Courses.Where(c => c.SemesterId == semesterId))
        //    //    .First()
        //    //    .Courses.AsQueryable();
        //}

        
    }
}
