using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.DTO;
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
        public IQueryable<Teacher> GetAll(bool containDeleted)
        {
            return containDeleted ? _context.Set<Teacher>().IgnoreQueryFilters() : _context.Set<Teacher>();
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

        /// <summary>
        /// Get courses for the teacher in the given semester
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <param name="containDeleted"></param>
        /// <returns></returns>
        public IQueryable<Course> GetAllByTeacherAndSemester(int id, int semesterId, bool containDeleted)
        {
            if (containDeleted)
            {
                var teacher = _context.Set<Teacher>()
                .Include(t => t.Courses)
                .ThenInclude(c => c.Subject)
                .IgnoreQueryFilters()
                .FirstOrDefault(t => t.Id == id);
                if (teacher == null) return null;
                return teacher.Courses.Where(c => c.SemesterId == semesterId).AsQueryable();
            }
            else
            {
                var teacher = _context.Set<Teacher>()
                .Include(t => t.Courses)
                .ThenInclude(c => c.Subject)
                .FirstOrDefault(t => t.Id == id);
                if (teacher == null) return null;
                return teacher.Courses.Where(c => c.SemesterId == semesterId).AsQueryable();
            }
        }

        /// <summary>
        /// Task 10 - Get students for teacher for a semester
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <returns></returns>
        public List<StudentDTO> GetAllStudentsByTeacherAndSemester(int id, int semesterId)
        {
            var teacher = _context.Set<Teacher>()
                .Include(t => t.Courses.Where(c => c.SemesterId == semesterId))
                    .ThenInclude(c => c.Subject)
                    .IgnoreQueryFilters()
                .Include(t => t.Courses)
                    .ThenInclude(c => c.Students)
                    .IgnoreQueryFilters()
                .IgnoreQueryFilters()
                .FirstOrDefault(t => t.Id == id);

            List<StudentDTO> result = new List<StudentDTO>();

            if(teacher != null)
            {
                teacher.Courses.ToList().ForEach(course =>
                {
                    course.Students.ToList().ForEach(student =>
                    {
                        result.Add(new StudentDTO()
                        {
                            Id = student.Id,
                            Name = student.Name,
                            NeptunId = student.NeptunId,
                            Subject = course.Subject
                        });
                    });
                });
            }

            return result.OrderBy(s => s.Subject.Code).ThenBy(s => s.Name).ToList();
        }

        /// <summary>
        /// Task 11 - Get teacher aggregated data per semester, teacher name, student count, credit count of courses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <returns></returns>
        public TeacherAggregateDTO GetTeacherAggregatedBySemester(int id, int semesterId)
        {
            var teacher = _context.Set<Teacher>()
                .Include(t => t.Courses.Where(c => c.SemesterId == semesterId))
                    .ThenInclude(c => c.Subject)
                    .IgnoreQueryFilters()
                .Include(t => t.Courses)
                    .ThenInclude(c => c.Students)
                    .IgnoreQueryFilters()
                .IgnoreQueryFilters()
                .FirstOrDefault(t => t.Id == id);

            var creditCounter = 0;
            teacher.Courses.ToList().ForEach(course =>
            {
                creditCounter += course.Subject.Credit;
            });

            var studentList = new List<Student>();
            teacher.Courses.ToList().ForEach(course =>
            {
                studentList.AddRange(course.Students);
            });

            TeacherAggregateDTO dto = new TeacherAggregateDTO()
            {
                Name = teacher.Name,
                CreditCount = creditCounter,
                StudentCount = studentList.Distinct().Count()
            };

            return dto;
        }
    }
}
