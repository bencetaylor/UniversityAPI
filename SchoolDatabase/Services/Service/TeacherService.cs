using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Model.Entity.User;
using SchoolDatabase.Services.Interface;
using SchoolDatabase.UnitOfWork;
using SchoolDatabase.UnitOfWork.Interface;

namespace SchoolDatabase.Services.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeacherUnitOfWork _teacherUnitOfWork;

        public TeacherService(IUnitOfWork unitOfWork, ITeacherUnitOfWork teacherUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _teacherUnitOfWork = teacherUnitOfWork;
        }

        public IQueryable<Teacher> GetTeachers(bool containDeleted)
        {
            return containDeleted ? _unitOfWork.GetRepository<Teacher>().GetAll().IgnoreQueryFilters()
                : _unitOfWork.GetRepository<Teacher>().GetAll();
        }

        public async Task<Teacher> GetTeacher(int id)
        {
            return await _unitOfWork.GetRepository<Teacher>().GetById(id);
        }

        public async Task UpdateTeacher(Teacher teacher)
        {
            _unitOfWork.GetRepository<Teacher>().Update(teacher);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateTeacher(Teacher teacher)
        {
            await _unitOfWork.GetRepository<Teacher>().Create(teacher);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTeacher(int id)
        {
            await _unitOfWork.GetRepository<Teacher>().DeleteSoft(id);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Get courses for the teacher in the given semester
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <param name="containDeleted"></param>
        /// <returns></returns>
        public IQueryable<Course> GetAllByTeacherAndSemester(int id, int semesterId, bool containDeleted)
        {
            return _teacherUnitOfWork.GetAllByTeacherAndSemester(id, semesterId, containDeleted);
        }

        /// <summary>
        /// Task 10 - Get students for teacher for a semester
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <returns></returns>
        public List<StudentDTO> GetAllStudentsByTeacherAndSemester(int id, int semesterId)
        {
            return _teacherUnitOfWork.GetAllStudentsByTeacherAndSemester(id, semesterId);
        }

        /// <summary>
        /// Task 11 - Get teacher aggregated data per semester, teacher name, student count, credit count of courses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <returns></returns>
        public TeacherAggregateDTO GetTeacherAggregatedBySemester(int id, int semesterId)
        {
            return _teacherUnitOfWork.GetTeacherAggregatedBySemester(id, semesterId);
        }

        public async Task AssignToCourse(CourseSubscribeDTO dto)
        {
            await _teacherUnitOfWork.AssignToCourse(dto);
        }
    }
}
