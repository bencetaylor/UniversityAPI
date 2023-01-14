using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services;

namespace SchoolDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SemesterController : Controller
    {
        private readonly ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [HttpGet("{containDeleted?}")]
        public IQueryable<Semester> all(bool containDeleted)
        {
            return _semesterService.GetSemesters(containDeleted);
        }

        [HttpGet("{id}")]
        public Semester get(int id)
        {
            return _semesterService.GetSemester(id);
        }

        [HttpPost]
        public async Task create(Semester semester)
        {
            await _semesterService.CreateSemester(semester);
        }

        [HttpPut]
        public async Task update(Semester semester)
        {
            await _semesterService.UpdateSemester(semester);
        }

        [HttpDelete("{id}")]
        public async Task delete(int id)
        {
            await _semesterService.DeleteSemester(id);
        }
    }
}
