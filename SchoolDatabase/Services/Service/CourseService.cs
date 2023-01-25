using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services.Interface;
using SchoolDatabase.UnitOfWork;
using SchoolDatabase.UnitOfWork.Interface;

namespace SchoolDatabase.Services.Service
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseUnitOfWork _courseUnitOfWork;

        public CourseService(IUnitOfWork unitOfWork, ICourseUnitOfWork courseUnitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _courseUnitOfWork = courseUnitOfWork;
        }

        /// <summary>
        /// Get a course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Course> GetCourse(int id)
        {
            return await _courseUnitOfWork.GetCourseById(id);
        }

        /// <summary>
        /// List all courses
        /// </summary>
        /// <returns></returns>
        public IQueryable<Course> GetCourses(bool containDeleted)
        {
            return containDeleted ? _unitOfWork.GetRepository<Course>().GetAll().IgnoreQueryFilters()
                 : _unitOfWork.GetRepository<Course>().GetAll();
        }

        /// <summary>
        /// Create a course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task CreateCourse(Course course)
        {
            await _unitOfWork.GetRepository<Course>().Create(course);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteCourse(int id)
        {
            await _unitOfWork.GetRepository<Course>().DeleteSoft(id);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Update a course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task UpdateCourse(Course course)
        {
            _unitOfWork.GetRepository<Course>().Update(course);
            await _unitOfWork.SaveChangesAsync();
        }

        public IQueryable<Course> GetCourseFilteredByTime(DateTime from, DateTime to)
        {
            return _courseUnitOfWork.GetCourseFilteredByTime(from, to);
        }
    }
}
