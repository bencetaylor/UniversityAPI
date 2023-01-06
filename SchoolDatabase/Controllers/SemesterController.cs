﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]  
        public IQueryable<Semester> all()
        {
            return _semesterService.GetSemesters();
        }

        [HttpGet("{id}")]
        public IQueryable<Semester> get(int id)
        {
            return _semesterService.GetSemesterById(id);
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
