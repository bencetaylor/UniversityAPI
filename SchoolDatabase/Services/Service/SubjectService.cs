using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Services.Interface;
using SchoolDatabase.UnitOfWork;

namespace SchoolDatabase.Services.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Subject> GetSubjects(bool containDeleted)
        {
            return containDeleted ? _unitOfWork.GetRepository<Subject>().GetAll().IgnoreQueryFilters()
                 : _unitOfWork.GetRepository<Subject>().GetAll();
        }

        public async Task<Subject> GetSubject(int id)
        {
            return await _unitOfWork.GetRepository<Subject>().GetById(id);
        }

        public async Task CreateSubject(Subject subject)
        {
            await _unitOfWork.GetRepository<Subject>().Create(subject);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSubject(Subject subject)
        {
            _unitOfWork.GetRepository<Subject>().Update(subject);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSubject(int id)
        {
            await _unitOfWork.GetRepository<Subject>().DeleteSoft(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
