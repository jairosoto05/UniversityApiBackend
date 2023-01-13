using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTO
{
    public class StudentReqDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public virtual ICollection<CourseMinDTO> Courses { get; set; } = new List<CourseMinDTO>();
    }
}
