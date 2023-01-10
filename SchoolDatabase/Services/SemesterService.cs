using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly SchoolAPIDbContext _context;

        public SemesterService(SchoolAPIDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List all semesters
        /// </summary>
        /// <returns></returns>
        public IQueryable<Semester> GetSemesters(bool containDeleted)
        {
            return containDeleted ? _context.Set<Semester>().IgnoreQueryFilters() : _context.Set<Semester>();
        }

        /// <summary>
        /// Get a semester by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Semester> GetSemesterById(int id)
        {
            return _context.Set<Semester>().ToList().Where(e => e.Id == id).AsQueryable();
        }

        /// <summary>
        /// Create a semester
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public async Task CreateSemester(Semester semester)
        {
            await _context.Set<Semester>().AddAsync(semester);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a semester by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteSemester(int id)
        {
            var semester = _context.Set<Semester>().FirstOrDefault(e => e.Id == id);
            if(semester != null)
            {
                semester.Deleted = true;
                _context.Set<Semester>().Update(semester);
                await _context.SaveChangesAsync();
            }

        }

        /// <summary>
        /// Update a semester
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public async Task UpdateSemester(Semester semester)
        {
            _context.Set<Semester>().Update(semester);
            await _context.SaveChangesAsync();
        }
    }
}
