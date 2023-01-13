﻿using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public interface ITeacherService
    {
        public IQueryable<Teacher> GetAll(bool containDeleted);
        public IQueryable<Teacher> GetTeacherById(int id);
        public Task CreateTeacher(Teacher teacher);
        public Task UpdateTeacher(Teacher teacher);
        public Task DeleteTeacher(int id);
        public IQueryable<Course> GetAllByTeacherAndSemester(int id, int semesterId, bool containDeleted);
        public List<StudentDTO> GetAllStudentsByTeacherAndSemester(int id, int semesterId);
        public TeacherAggregateDTO GetTeacherAggregatedBySemester(int id, int semesterId);
    }
}
