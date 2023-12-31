using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartService.Migrations
{
    /// <inheritdoc />
    public partial class newColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "ProductCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "ProductCarts");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
