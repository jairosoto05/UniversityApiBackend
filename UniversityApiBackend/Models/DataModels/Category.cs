using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Category: BaseEntity
    {
        public string? Name { get; set; } = string.Empty;

        public virtual ICollection<Course>? Courses { get; set; }
    }
}
