using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Category: BaseEntity
    {
        [Required]
        public string? CategoryName { get; set; } = string.Empty;

        [Required]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
