using Microsoft.EntityFrameworkCore;

namespace DataAnnotation.Models
{
    public class QLBanHangContext : DbContext
    {
        public QLBanHangContext() { }
        public QLBanHangContext(DbContextOptions<QLBanHangContext> options) : base(options) { }
        public DbSet<HangHoa>? HangHoas { get; set; }
        public DbSet<LoaiHang>? LoaiHangs { get; set; }

    }
}
