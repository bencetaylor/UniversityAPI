using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Model.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NeptunId { get; set; }
        public Subject Subject { get; set; }
    }
}
