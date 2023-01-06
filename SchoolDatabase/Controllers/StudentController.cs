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

        [HttpGet]
        public IQueryable<Student> all()
        {
            return _studentService.GetStudents();
        }

        [HttpGet]
        [Route("{id}")]
        public IQueryable<Student> get(int id)
        {
            return _studentService.GetStudent(id);
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
