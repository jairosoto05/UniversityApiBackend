using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.DTO
{
    public class UserResDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
