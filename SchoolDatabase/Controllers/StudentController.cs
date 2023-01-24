using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Model.Entity.User;
using SchoolDatabase.Services;

namespace SchoolDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{containDeleted?}")]
        public IQueryable<Student> all(bool containDeleted)
        {
            return _studentService.GetStudents(containDeleted);
        }

        [HttpGet]
        [Route("{id}")]
        public Student get(int id)
        {
            return _studentService.GetStudent(id);
        }

        [HttpGet]
        [Route("{id}/courses/{semesterId}/{containDeleted?}")]
        public IQueryable<Course> get(int id, int semesterId, bool containDeleted)
        {
            return _studentService.GetAllByStudentAndSemester(id, semesterId, containDeleted);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task update(Student Student)
        {
            await _studentService.UpdateStudent(Student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task create(Student Student)
        {
            await _studentService.CreateStudent(Student);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task delete(int id)
        {
            await _studentService.DeleteStudentById(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task subscribe(CourseSubscribeDTO dto) 
        {
            await _studentService.AssignToCourse(dto);
        }
    }
}
