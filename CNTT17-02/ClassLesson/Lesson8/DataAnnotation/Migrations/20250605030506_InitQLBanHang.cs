using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAnnotation.Migrations
{
    /// <inheritdoc />
    public partial class InitQLBanHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiHang",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHang", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    MaHang = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    TenHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonGia = table.Column<string>(type: "varchar(64)", nullable: false),
                    MaLoai = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.MaHang);
                    table.ForeignKey(
                        name: "FK_HangHoa_LoaiHang_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "LoaiHang",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_MaLoai",
                table: "HangHoa",
                column: "MaLoai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HangHoa");

            migrationBuilder.DropTable(
                name: "LoaiHang");
        }
    }
}
