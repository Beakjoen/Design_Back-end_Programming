using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaiTapLon.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBannerFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Banners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Banners",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Banners");
        }
    }
}
