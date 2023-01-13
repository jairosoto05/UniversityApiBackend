using System.ComponentModel.DataAnnotations;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTO
{
    public class CourseReqDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Level Level { get; set; } = Level.Basic;
        public virtual CategoryMinDTO? Category { get; set; }
        public virtual ICollection<StudentMinDTO> Students { get; set; } = new List<StudentMinDTO>();
    }
}
