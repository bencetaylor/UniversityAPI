using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly SchoolAPIDbContext _schoolAPIDbContext;

        public TeacherService(SchoolAPIDbContext schoolAPIDbContext)
        {
            _schoolAPIDbContext = schoolAPIDbContext;
        }

        /// <summary>
        /// Get all teachers using db context
        /// </summary>
        public IQueryable<Teacher> GetAll()
        {
            return _schoolAPIDbContext.Set<Teacher>();
        }

        public IQueryable<Subject> GetSubjectsByTeacherAndSemester(int TeacherId, int SemesterId)
        {
            throw new NotImplementedException();
        }
    }
}
