using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NguyenQuocHuy.Models
{
    public class UserContext : DbContext
    {
        public UserContext() { }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
