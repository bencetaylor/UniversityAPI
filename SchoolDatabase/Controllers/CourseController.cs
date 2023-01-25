using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services.Interface;

namespace SchoolDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("{containDeleted?}")]
        [AllowAnonymous]
        public IQueryable<Course> all(bool containDeleted)
        {
            return _courseService.GetCourses(containDeleted);
        }

        [HttpGet("{id}")]
        public async Task<Course> get(int id)
        {
            return await _courseService.GetCourse(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task create(Course course)
        {
            await _courseService.CreateCourse(course);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task update(Course course)
        {
            await _courseService.UpdateCourse(course);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task delete(int id)
        {
            await _courseService.DeleteCourse(id);
        }

        [HttpGet]
        public IQueryable<Course> filter([FromBody] CourseFilterDTO filter)
        {
            DateTime from = new DateTime(2022, 5, 20);
            DateTime to = new DateTime(2023, 10, 1);
            return _courseService.GetCourseFilteredByTime(from, to);
        }
    }
}
