using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTO
{
    public class CourseResDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Level Level { get; set; } = Level.Basic;
    }
}
