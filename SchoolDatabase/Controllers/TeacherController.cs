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

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public IQueryable<Teacher> all()
        {
            return _teacherService.GetAll();
        }

        [HttpGet("{TeacherId}/{SemesterId}")]
        public IQueryable<Subject> teachers(int TeacherId, int SemesterId)
        {
            return _teacherService.GetSubjectsByTeacherAndSemester(TeacherId, SemesterId);
        }
    }
}
