using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services;

namespace SchoolDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IQueryable<Course> all()
        {
            return _courseService.GetCourses();
        }

        [HttpGet("{id}")]
        public IQueryable<Course> get(int id)
        {
            return _courseService.GetCourse(id);
        }

        [HttpPost]
        public async Task create(Course course)
        {
            await _courseService.CreateCourse(course);
        }

        [HttpPut]
        public async Task update(Course course)
        {
            await _courseService.UpdateCourse(course);
        }

        [HttpDelete("{id}")]
        public async Task delete(int id)
        {
            await _courseService.DeleteCourse(id);
        }
    }
}
