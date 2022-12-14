using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public string TargetAudiences { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public enum Level
        {
            Basic,
            Intermediate,
            Advanced
        }

    }
}
