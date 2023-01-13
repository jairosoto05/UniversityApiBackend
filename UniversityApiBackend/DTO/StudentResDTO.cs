using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTO
{
    public class StudentResDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public virtual ICollection<CourseMinDTO> Courses { get; set; } = new List<CourseMinDTO>();
    }
}
