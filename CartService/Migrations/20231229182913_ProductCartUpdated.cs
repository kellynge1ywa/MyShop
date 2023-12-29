using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartService.Migrations
{
    /// <inheritdoc />
    public partial class ProductCartUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductCarts_ProductId",
                table: "ProductCarts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_ProductDto_ProductId",
                table: "ProductCarts",
                column: "ProductId",
                principalTable: "ProductDto",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_ProductDto_ProductId",
                table: "ProductCarts");

            migrationBuilder.DropIndex(
                name: "IX_ProductCarts_ProductId",
                table: "ProductCarts");
        }
    }
}
