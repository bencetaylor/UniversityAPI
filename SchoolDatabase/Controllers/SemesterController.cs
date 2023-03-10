using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services.Interface;

namespace SchoolDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<Semester> get(int id)
        {
            return await _semesterService.GetSemester(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task create(Semester semester)
        {
            await _semesterService.CreateSemester(semester);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task update(Semester semester)
        {
            await _semesterService.UpdateSemester(semester);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task delete(int id)
        {
            await _semesterService.DeleteSemester(id);
        }
    }
}
