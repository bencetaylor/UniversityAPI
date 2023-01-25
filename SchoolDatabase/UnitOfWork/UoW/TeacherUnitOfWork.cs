using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Model.Entity.User;
using SchoolDatabase.UnitOfWork.Interface;

namespace SchoolDatabase.UnitOfWork.UoW
{
    public class TeacherUnitOfWork : UnitOfWork, ITeacherUnitOfWork
    {

        public TeacherUnitOfWork(SchoolAPIDbContext context) : base(context)
        {
        }

        public IQueryable<Course> GetAllByTeacherAndSemester(int id, int semesterId, bool containDeleted)
        {
            if (containDeleted)
            {
                var teacher = GetRepository<Teacher>().GetAll()
                .Include(t => t.Courses)
                .ThenInclude(c => c.Subject)
                .IgnoreQueryFilters()
                .FirstOrDefault(t => t.Id == id);
                if (teacher == null) throw new Exception("No teacher found for id: " + id + "!");
                return teacher.Courses.Where(c => c.SemesterId == semesterId).AsQueryable();
            }
            else
            {
                var teacher = GetRepository<Teacher>().GetAll()
                .Include(t => t.Courses)
                .ThenInclude(c => c.Subject)
                .FirstOrDefault(t => t.Id == id);
                if (teacher == null) throw new Exception("No teacher found for id: " + id + "!");
                return teacher.Courses.Where(c => c.SemesterId == semesterId).AsQueryable();
            }
        }

        public List<StudentDTO> GetAllStudentsByTeacherAndSemester(int id, int semesterId)
        {
            var teacher = GetRepository<Teacher>().GetAll()
               .Include(t => t.Courses.Where(c => c.SemesterId == semesterId))
                   .ThenInclude(c => c.Subject)
                   .IgnoreQueryFilters()
               .Include(t => t.Courses)
                   .ThenInclude(c => c.Students)
                   .IgnoreQueryFilters()
               .IgnoreQueryFilters()
               .FirstOrDefault(t => t.Id == id);

            List<StudentDTO> result = new List<StudentDTO>();

            if (teacher == null) throw new Exception("No teacher found for id: " + id + "!");
            else
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

        public TeacherAggregateDTO GetTeacherAggregatedBySemester(int id, int semesterId)
        {
            var teacher = GetRepository<Teacher>().GetAll()
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

        public async Task AssignToCourse(CourseSubscribeDTO dto)
        {
            var course = GetDbSet<Course>()
                .Include(c => c.Teachers)
                .FirstOrDefault(c => c.Id == dto.CourseId);

            var teacher = course.Teachers.SingleOrDefault(t => t.Id == dto.UserId);
            course.Teachers.Remove(teacher);
            _context.Set<Course>().Update(course);

            //var teacher = GetDbSet<Teacher>()
            //    .FirstOrDefault(e => e.Id == dto.UserId);
            //var course = GetDbSet<Course>()
            //    .Include(c => c.Teachers)
            //    .FirstOrDefault(e => e.Id == dto.CourseId);
            //if (teacher != null && course != null)
            //{
            //    course.Teachers.Add(teacher);
            //    GetDbSet<Course>().Update(course);
            //}
        }
    }
}
