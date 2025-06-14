using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HocDBFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitSchoolDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    MaPhong = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TenPhong = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhongBan__20BD5E5B8B05B2B2", x => x.MaPhong);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MaPhong = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__2725D70A876F1AB0", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK__NhanVien__MaPhon__398D8EEE",
                        column: x => x.MaPhong,
                        principalTable: "PhongBan",
                        principalColumn: "MaPhong");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaPhong",
                table: "NhanVien",
                column: "MaPhong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "PhongBan");
        }
    }
}
