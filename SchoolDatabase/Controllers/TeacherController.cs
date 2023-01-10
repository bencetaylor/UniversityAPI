using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services;

namespace SchoolDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        //private readonly ICourseService _courseService;


        public TeacherController(ITeacherService teacherService/*, ICourseService courseService*/)
        {
            _teacherService = teacherService;
            //_courseService = courseService;
        }

        [HttpGet("{containDeleted?}")]
        public IQueryable<Teacher> all(bool containDeleted)
        {
            return _teacherService.GetAll(containDeleted);
        }

        [HttpGet("{id}")]
        public IQueryable<Teacher> get(int id) {
            return _teacherService.GetTeacherById(id);
        }

        [HttpGet("{id}/courses/{semesterId}/{containDeleted?}")]
        public IQueryable<Course> get(int id, int semesterId, bool containDeleted)
        {
            return _teacherService.GetAllByTeacherAndSemester(id, semesterId, containDeleted);
        }

        [HttpPost]
        public async Task create([FromBody] Teacher teacher)
        {
            await _teacherService.CreateTeacher(teacher);
        }

        [HttpPut]
        public async Task update(Teacher teacher)
        {
            await _teacherService.UpdateTeacher(teacher);
        }

        [HttpDelete("{id}")]
        public async Task delete(int id)
        {
            await _teacherService.DeleteTeacher(id);
        }

    }
}
