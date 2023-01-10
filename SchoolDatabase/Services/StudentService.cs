using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolAPIDbContext _context;

        public StudentService(SchoolAPIDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List students based on the deleted flag
        /// </summary>
        /// <returns></returns>
        public IQueryable<Student> GetStudents(bool containDeleted)
        {
            return containDeleted ? _context.Set<Student>().IgnoreQueryFilters() : _context.Set<Student>();
        }

        /// <summary>
        /// Get a student by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IQueryable<Student> GetStudent(int Id)
        {
            return _context.Set<Student>().ToList().Where(e => e.Id == Id).AsQueryable();
        }

        /// <summary>
        /// Update a student entity
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        public async Task UpdateStudent(Student Student)
        {
            _context.Set<Student>().Update(Student);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Create a student entity
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        public async Task CreateStudent(Student Student)
        {
            await _context.Set<Student>().AddAsync(Student);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteStudentById(int id)
        {
            var student = _context.Set<Student>().FirstOrDefault(e => e.Id == id);
            if (student != null)
            {
                student.Deleted = true;
                _context.Set<Student>().Update(student);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <param name="containDeleted"></param>
        /// <returns></returns>
        IQueryable<Course> IStudentService.GetAllByStudentAndSemester(int id, int semesterId, bool containDeleted)
        {
            if(containDeleted)
            {
                var student = _context.Set<Student>()
                .Include(s => s.Courses)
                .ThenInclude(c => c.Subject)
                .IgnoreQueryFilters()
                .FirstOrDefault(t => t.Id == id);
                if (student == null) return null;

                return student.Courses.Where(c => c.SemesterId == semesterId).AsQueryable();
            } 
            else
            {
                var student = _context.Set<Student>()
                .Include(s => s.Courses)
                .ThenInclude(c => c.Subject)
                .FirstOrDefault(t => t.Id == id);
                if (student == null) return null;

                return student.Courses.Where(c => c.SemesterId == semesterId).AsQueryable();
            }
        }
    }
}
