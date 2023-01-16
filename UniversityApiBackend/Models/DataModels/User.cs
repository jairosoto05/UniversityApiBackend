using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace UniversityApiBackend.Models.DataModels
{
    public enum Rol
    {
        Admin,
        Teacher
    }

    [Index(nameof(Username), IsUnique = true)]
    public class User: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string? Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public Rol Rol { get; set; } = Rol.Teacher;
    }
}
