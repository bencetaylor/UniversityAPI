using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.DTO;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Model.Entity.User;
using SchoolDatabase.UnitOfWork;

namespace SchoolDatabase.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentUnitOfWork _studentUnitOfWork;

        public StudentService(IUnitOfWork unitOfWork, IStudentUnitOfWork studentUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentUnitOfWork = studentUnitOfWork;
        }

        /// <summary>
        /// List students based on the deleted flag
        /// </summary>
        /// <returns></returns>
        public IQueryable<Student> GetStudents(bool containDeleted)
        {
            return containDeleted ? _unitOfWork.GetRepository<Student>().GetAll().IgnoreQueryFilters()
                : _unitOfWork.GetRepository<Student>().GetAll();
        }

        /// <summary>
        /// Get a student by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Student GetStudent(int id)
        {
            return _unitOfWork.GetDbSet<Student>()
                .Include(s => s.Courses)
                    .ThenInclude(c => c.Subject)
                .Include(s => s.Speciality)
                .FirstOrDefault(e => e.Id == id);

        }

        /// <summary>
        /// Update a student entity
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        public async Task UpdateStudent(Student Student)
        {
            _unitOfWork.GetRepository<Student>().Update(Student);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Create a student entity
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        public async Task CreateStudent(Student Student)
        {
            await _unitOfWork.GetRepository<Student>().Create(Student);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteStudentById(int id)
        {
            await _unitOfWork.GetRepository<Student>().DeleteSoft(id);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="semesterId"></param>
        /// <param name="containDeleted"></param>
        /// <returns></returns>
        public IQueryable<Course> GetAllByStudentAndSemester(int id, int semesterId, bool containDeleted)
        {
            return _studentUnitOfWork.GetAllByStudentAndSemester(id, semesterId, containDeleted);
        }

        public async Task AssignToCourse(CourseSubscribeDTO dto)
        {
            await _studentUnitOfWork.AssignToCourse(dto);
        }
    }
}
