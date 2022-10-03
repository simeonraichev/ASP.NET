using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopDemo.Core.Migrations
{
    public partial class ItemsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Primary key"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Name of the item"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price of the item"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of the item")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                },
                comment: "Products to sell");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
