using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartService.Migrations
{
    /// <inheritdoc />
    public partial class ProductCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ProductDto_ProductsProductId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductsProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Carts");

            migrationBuilder.CreateTable(
                name: "ProductCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductResponseDto",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductResponseDto", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductResponseDto_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductResponseDto_CartId",
                table: "ProductResponseDto",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCarts");

            migrationBuilder.DropTable(
                name: "ProductResponseDto");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Carts",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductsProductId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Carts",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Carts",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductsProductId",
                table: "Carts",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ProductDto_ProductsProductId",
                table: "Carts",
                column: "ProductsProductId",
                principalTable: "ProductDto",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
