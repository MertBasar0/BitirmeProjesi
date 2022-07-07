using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class MigTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Customer_BasketOfCustomerId",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_BasketOfCustomerId",
                table: "Basket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Basket_BasketOfCustomerId",
                table: "Basket",
                column: "BasketOfCustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Customer_BasketOfCustomerId",
                table: "Basket",
                column: "BasketOfCustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
