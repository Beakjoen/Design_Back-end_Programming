using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaiTapLon.Migrations
{
    /// <inheritdoc />
    public partial class AddSalePercentToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalePercent",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalePercent",
                table: "Products");
        }
    }
}
