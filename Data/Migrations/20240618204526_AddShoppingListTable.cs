using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopList.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingListTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingListId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ShoppingList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlannedPurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingList", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ShoppingListId",
                table: "Product",
                column: "ShoppingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ShoppingList_ShoppingListId",
                table: "Product",
                column: "ShoppingListId",
                principalTable: "ShoppingList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ShoppingList_ShoppingListId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ShoppingList");

            migrationBuilder.DropIndex(
                name: "IX_Product_ShoppingListId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ShoppingListId",
                table: "Product");
        }
    }
}
