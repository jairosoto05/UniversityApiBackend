using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTO
{
    public class StudentMinDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
