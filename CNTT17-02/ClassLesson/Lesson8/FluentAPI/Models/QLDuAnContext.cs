using Microsoft.EntityFrameworkCore;

namespace FluentAPI.Models
{
    public class QLDuAnContext : DbContext
    {
        public QLDuAnContext() { }

        public QLDuAnContext(DbContextOptions<QLDuAnContext> options) : base(options) { }
        //Khai báo tập các thưc thể
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cấu hình thực thể PhongBan
            modelBuilder.Entity<PhongBan>(entity =>
            {
                //Thiết lập cấu hình tên bảng
                entity.ToTable("PhongBan");
                //Thiết lập khóa chính
                entity.HasKey(e => e.MaPhong);
                //Thiết lập các thuộc tính
                entity.Property(e => e.MaPhong)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(20)
                    .IsRequired();
                //Thiết lập thuộc tính tên phòng
                entity.Property(e => e.TenPhong)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            //Cấu hình thực thể NhanVien
            modelBuilder.Entity<NhanVien>(entity =>
            {
                //Thiết lập cấu hình tên bảng
                entity.ToTable("NhanVien");
                //Thiết lập khóa chính
                entity.HasKey(e => e.MaNV);
                //Thiết lập các thuộc tính
                entity.Property(e => e.MaNV)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(20)
                    .IsRequired();
                //Thiết lập thuộc tính họ tên
                entity.Property(e => e.HoTen)
                    .HasMaxLength(100)
                    .IsRequired();
                //Thiết lập quan hệ với PhongBan
                entity.HasOne(e => e.PhongBan)
                    .WithMany(p => p.NhanVien)
                    .HasForeignKey(e => e.MaPhong);
            });
        }
    }
}