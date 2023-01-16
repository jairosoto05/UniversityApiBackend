using Microsoft.Build.Framework;

namespace UniversityApiBackend.Models.DataModels
{
    public class LoginUser
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
