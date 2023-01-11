using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.DTO;
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

        /// <summary>
        /// List all courses the teacher has in the semester
        /// optional - show deleted
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <param name="containDeleted"></param>
        /// <returns></returns>
        [HttpGet("{id}/courses/{semesterId}/{containDeleted?}")]
        public IQueryable<Course> get(int id, int semesterId, bool containDeleted)
        {
            return _teacherService.GetAllByTeacherAndSemester(id, semesterId, containDeleted);
        }

        /// <summary>
        /// List all students in the selected semester for the teacher
        /// The elements contains the students' name, neptun id, subject
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <returns></returns>
        [HttpGet("{id}/students/{semesterId}")]
        public List<StudentDTO> get(int id, int semesterId)
        {
            return _teacherService.GetAllStudentsByTeacherAndSemester(id, semesterId);
        }
    }
}
