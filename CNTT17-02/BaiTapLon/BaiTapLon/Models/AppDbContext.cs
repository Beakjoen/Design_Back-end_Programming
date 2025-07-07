using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BaiTapLon.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            modelBuilder.Entity<Promotion>()
                .HasIndex(p => p.Code)
                .IsUnique();
            modelBuilder.Entity<Product>()
                .Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .Property(p => p.DiscountPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.DiscountUnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.TotalLine).HasColumnType("decimal(18,2)");
        }

        public static void SeedData(AppDbContext context)
        {
            // Seed admin user
            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var admin = new User
                {
                    UserName = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    FullName = "Quản trị viên",
                    Email = "nguyenhuy2k05@gmail.com",
                    IsAdmin = true,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                context.Users.Add(admin);
                context.SaveChanges();
            }

            // Seed user Himeko
            if (!context.Users.Any(u => u.UserName == "himeko"))
            {
                var himeko = new User
                {
                    UserName = "himeko",
                    Password = BCrypt.Net.BCrypt.HashPassword("himeko123"),
                    FullName = "Himeko",
                    Email = "beakjoen@gmail.com",
                    IsAdmin = false,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                context.Users.Add(himeko);
                context.SaveChanges();
            }

            // Update admin email if exists
            var adminUser = context.Users.FirstOrDefault(u => u.UserName == "admin");
            if (adminUser != null && adminUser.Email != "nguyenhuy2k05@gmail.com")
            {
                adminUser.Email = "nguyenhuy2k05@gmail.com";
                context.SaveChanges();
            }
            // Update Himeko email if exists
            var himekoUser = context.Users.FirstOrDefault(u => u.UserName == "himeko");
            if (himekoUser != null && himekoUser.Email != "beakjoen@gmail.com")
            {
                himekoUser.Email = "beakjoen@gmail.com";
                context.SaveChanges();
            }

            // Seed categories
            if (!context.Categories.Any())
            {
                var cat1 = new Category { Name = "Quạt trần Panasonic", Description = "Quạt trần thương hiệu Panasonic", DisplayOrder = 1 };
                var cat2 = new Category { Name = "Quạt trần Mitsubishi", Description = "Quạt trần thương hiệu Mitsubishi", DisplayOrder = 2 };
                var cat3 = new Category { Name = "Quạt trần KDK", Description = "Quạt trần thương hiệu KDK", DisplayOrder = 3 };
                context.Categories.AddRange(cat1, cat2, cat3);
                context.SaveChanges();
            }

            // Seed products
            if (!context.Products.Any())
            {
                var panasonic = context.Categories.FirstOrDefault(c => c.Name.Contains("Panasonic"));
                var kdk = context.Categories.FirstOrDefault(c => c.Name.Contains("KDK"));
                var khac = context.Categories.FirstOrDefault(c => c.Name.Contains("Mitsubishi"));
                if (panasonic != null && kdk != null && khac != null)
                {
                    context.Products.AddRange(
                        new Product { Name = "Quạt trần KDK U60FW 5 cánh", Description = "Quạt trần KDK U60FW 5 cánh, điều khiển, có đèn LED.", Price = 4200000, DiscountPrice = 3990000, ImageUrl = "/image/quat-tran-kdk-u60fw-5-canh-9-toc-do-co-dieu-khien-co-den-led.jpg", Stock = 10, Category = kdk, Status = "Còn hàng", IsFeatured = true },
                        new Product { Name = "Quạt trần Nanoco NCFD5251", Description = "Quạt trần trang trí 5 cánh Nanoco NCFD5251.", Price = 3500000, DiscountPrice = 3200000, ImageUrl = "/image/quat-tran-trang-tri-5-canh-nanoco-ncfd5251.jpg", Stock = 8, Category = khac, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần cánh gỗ Kim Thuận Phong KTP15G", Description = "Quạt trần cánh gỗ Kim Thuận Phong KTP15G.", Price = 3700000, DiscountPrice = 3500000, ImageUrl = "/image/quat-tran-canh-go-kim-thuan-phong-ktp15g.jpg", Stock = 7, Category = khac, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần đa năng USB Tiross TS2287", Description = "Quạt trần đa năng USB Tiross TS2287.", Price = 650000, DiscountPrice = 590000, ImageUrl = "/image/quat-tran-da-nang-usb-tiross-ts2287.jpg", Stock = 20, Category = khac, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần đèn chùm cánh gỗ Kim Thuận Phong BT586I", Description = "Quạt trần đèn chùm cánh gỗ Kim Thuận Phong BT586I 5 cánh.", Price = 5200000, DiscountPrice = 4990000, ImageUrl = "/image/quat-tran-den-chum-canh-go-kim-thuan-phong-bt586i-5-canh.jpg", Stock = 5, Category = khac, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần Panasonic F-60MZ2-S", Description = "Quạt trần Panasonic F-60MZ2-S.", Price = 2100000, DiscountPrice = 1990000, ImageUrl = "/image/quat-tran-panasonic-f-60mz2-s.jpg", Stock = 15, Category = panasonic, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần Panasonic F-56MZG 4 cánh", Description = "Quạt trần Panasonic F-56MZG 4 cánh, điều khiển từ xa.", Price = 2500000, DiscountPrice = 2350000, ImageUrl = "/image/quat-tran-panasonic-f-56mzg-4-canh-co-dieu-khien-tu-xa.jpg", Stock = 12, Category = panasonic, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần Panasonic F-60GDS 5 cánh", Description = "Quạt trần 5 cánh Panasonic F-60GDS màu nâu.", Price = 3200000, DiscountPrice = 2990000, ImageUrl = "/image/quat-tran-5-canh-panasonic-f-60gds-nau.jpg", Stock = 10, Category = panasonic, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần Vinawind QT1500X", Description = "Quạt trần Vinawind QT1500X.", Price = 1200000, DiscountPrice = 1100000, ImageUrl = "/image/quat-tran-vinawind-qt1500x.jpg", Stock = 18, Category = khac, Status = "Còn hàng" },
                        new Product { Name = "Quạt ốp trần Dasin MY888C-G", Description = "Quạt ốp trần Dasin MY888C-G.", Price = 1850000, DiscountPrice = 1750000, ImageUrl = "/image/quat-op-tran-dasin-my888c-g.jpg", Stock = 9, Category = khac, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần Panasonic 4 cánh F-56XPG", Description = "Quạt trần Panasonic 4 cánh F-56XPG.", Price = 2300000, DiscountPrice = 2150000, ImageUrl = "/image/quat-tran-panasonic-4-canh-f-56xpg.jpg", Stock = 13, Category = panasonic, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần Panasonic F-60MZ2", Description = "Quạt trần Panasonic F-60MZ2.", Price = 2100000, DiscountPrice = 1990000, ImageUrl = "/image/quat-tran-panasonic-f-60mz2.jpg", Stock = 14, Category = panasonic, Status = "Còn hàng" },
                        new Product { Name = "Quạt trần Vinawind QT1400-S", Description = "Quạt trần Vinawind QT1400-S, sải cánh 1400mm, cánh sắt.", Price = 1150000, DiscountPrice = 1050000, ImageUrl = "/image/quat-tran-vinawind-qt1400-s-sai-canh-1400-canh-sat.jpg", Stock = 16, Category = khac, Status = "Còn hàng" }
                    );
                    context.SaveChanges();
                }
            }

            // Seed promotions
            if (!context.Promotions.Any())
            {
                context.Promotions.AddRange(
                    new Promotion {
                        Code = "SALE10",
                        Name = "Giảm 10% cho đơn từ 1 triệu",
                        Description = "Áp dụng cho tất cả sản phẩm",
                        DiscountPercent = 10,
                        MinOrderAmount = 1000000,
                        MaxDiscountAmount = 500000,
                        MaxUsage = 100,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(30),
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new Promotion {
                        Code = "FREESHIP",
                        Name = "Miễn phí vận chuyển toàn quốc",
                        Description = "Miễn phí vận chuyển cho đơn hàng từ 2 triệu",
                        DiscountAmount = 50000,
                        MinOrderAmount = 2000000,
                        MaxUsage = 50,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(15),
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    }
                );
                context.SaveChanges();
            }

            // Seed banners
            if (!context.Banners.Any())
            {
                context.Banners.AddRange(
                    new Banner { ImageUrl = "/image/banner1.jpg", Link = "#", Description = "Khuyến mãi lớn mùa hè", IsActive = true, DisplayOrder = 1 },
                    new Banner { ImageUrl = "/image/banner2.jpg", Link = "#", Description = "Miễn phí vận chuyển toàn quốc", IsActive = true, DisplayOrder = 2 },
                    new Banner { ImageUrl = "/image/banner3.jpg", Link = "#", Description = "Sản phẩm mới về", IsActive = true, DisplayOrder = 3 }
                );
                context.SaveChanges();
            }
        }
    }
} 