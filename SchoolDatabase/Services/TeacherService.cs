using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly SchoolAPIDbContext _context;

        public TeacherService(SchoolAPIDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all teachers using db context
        /// </summary>
        public IQueryable<Teacher> GetAll()
        {
            return _context.Set<Teacher>();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Teacher> GetTeacherById(int id)
        {
            return _context.Set<Teacher>()
                .Where(e => e.Id == id)
                .Include(e => e.Position)
                .Include(e => e.Courses)
                .AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public async Task UpdateTeacher(Teacher teacher)
        {
            _context.Set<Teacher>().Update(teacher);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public async Task CreateTeacher(Teacher teacher)
        {
            await _context.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteTeacher(int id)
        {
            var teacher = _context.Set<Teacher>().FirstOrDefault(e => e.Id == id);
            if(teacher != null)
            {
                teacher.Deleted = true;
                _context.Set<Teacher>().Update(teacher);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<Course> GetAllByTeacherAndSemester(int id, int semesterId)
        {
            var teacher = _context.Set<Teacher>().FirstOrDefault(t => t.Id == id);
            if (teacher == null) return null;
            return teacher.Courses.Where(c => c.SemesterId == semesterId).AsQueryable();
        }
    }
}
