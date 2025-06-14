using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() { }
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }   
    }
}
