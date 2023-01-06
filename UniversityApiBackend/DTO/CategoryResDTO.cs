using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTO
{
    public class CategoryResDTO
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; } = string.Empty;
        public virtual ICollection<CourseResDTO> Courses { get; set; } = new List<CourseResDTO>();
    }
}
