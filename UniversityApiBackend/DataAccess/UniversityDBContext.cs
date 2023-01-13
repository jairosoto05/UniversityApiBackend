using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options): base(options)
        {

        }
        public DbSet<User>? USERS { get; set; }
        public DbSet<Course>? COURSES { get; set; }
        public DbSet<Category>? CATEGORIES { get; set; }
        public DbSet<Student>? STUDENTS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Course>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Courses)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
