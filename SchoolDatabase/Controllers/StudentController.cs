using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services;

namespace SchoolDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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
        public async Task update(Student Student)
        {
            await _studentService.UpdateStudent(Student);
        }

        [HttpPost]
        public async Task create(Student Student)
        {
            await _studentService.CreateStudent(Student);
        }

        [HttpDelete("{id}")]
        public async Task delete(int id)
        {
            await _studentService.DeleteStudentById(id);
        }
    }
}
