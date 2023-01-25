using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services;
using SchoolDatabase.Services.Interface;

namespace SchoolDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubjectController
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("{containDeleted?}")]
        public IQueryable<Subject> all(bool containDeleted)
        {
            return _subjectService.GetSubjects(containDeleted);
        }

        [HttpGet("{id}")]
        public async Task<Subject> get(int id)
        {
            return await _subjectService.GetSubject(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task create(Subject subject)
        {
            await _subjectService.CreateSubject(subject);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task update(Subject subject)
        {
            await _subjectService.UpdateSubject(subject);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task delete(int id)
        {
            await _subjectService.DeleteSubject(id);
        }

    }
}
