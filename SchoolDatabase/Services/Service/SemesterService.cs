using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services.Interface;
using SchoolDatabase.UnitOfWork;

namespace SchoolDatabase.Services.Service
{
    public class SemesterService : ISemesterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SemesterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// List all semesters
        /// </summary>
        /// <returns></returns>
        public IQueryable<Semester> GetSemesters(bool containDeleted)
        {
            return containDeleted ? _unitOfWork.GetRepository<Semester>().GetAll().IgnoreQueryFilters()
                : _unitOfWork.GetRepository<Semester>().GetAll();
        }

        /// <summary>
        /// Get a semester by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Semester> GetSemester(int id)
        {
            return await _unitOfWork.GetRepository<Semester>().GetById(id);
        }

        /// <summary>
        /// Create a semester
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public async Task CreateSemester(Semester semester)
        {
            await _unitOfWork.GetRepository<Semester>().Create(semester);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a semester by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteSemester(int id)
        {
            await _unitOfWork.GetRepository<Semester>().DeleteSoft(id);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Update a semester
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public async Task UpdateSemester(Semester semester)
        {
            _unitOfWork.GetRepository<Semester>().Update(semester);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
