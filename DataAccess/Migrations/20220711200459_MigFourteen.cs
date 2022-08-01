using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class MigFourteen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sepetler_Musteriler_CustomerId",
                table: "Sepetler");

            migrationBuilder.DropIndex(
                name: "IX_Sepetler_CustomerId",
                table: "Sepetler");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Sepetler",
                newName: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Sepetler_CustomerID",
                table: "Sepetler",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Sepetler_Musteriler_CustomerID",
                table: "Sepetler",
                column: "CustomerID",
                principalTable: "Musteriler",
                principalColumn: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sepetler_Musteriler_CustomerID",
                table: "Sepetler");

            migrationBuilder.DropIndex(
                name: "IX_Sepetler_CustomerID",
                table: "Sepetler");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Sepetler",
                newName: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sepetler_CustomerId",
                table: "Sepetler",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Sepetler_Musteriler_CustomerId",
                table: "Sepetler",
                column: "CustomerId",
                principalTable: "Musteriler",
                principalColumn: "CustomerId");
        }
    }
}
