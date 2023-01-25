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
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("{containDeleted?}")]
        public IQueryable<Teacher> all(bool containDeleted)
        {
            return _teacherService.GetTeachers(containDeleted);
        }

        [HttpGet("{id}")]
        public async Task<Teacher> get(int id) {
            return await _teacherService.GetTeacher(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task create(Teacher teacher)
        {
            await _teacherService.CreateTeacher(teacher);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task update(Teacher teacher)
        {
            await _teacherService.UpdateTeacher(teacher);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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

        [HttpGet("{id}/{semesterId}")]
        public TeacherAggregateDTO aggregate(int id, int semesterId)
        {
            return _teacherService.GetTeacherAggregatedBySemester(id, semesterId);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task subscribe(CourseSubscribeDTO dto)
        {
            await _teacherService.AssignToCourse(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task unsubscribe(CourseSubscribeDTO dto)
        {
            await _teacherService.AssignToCourse(dto);
        }
    }
}
