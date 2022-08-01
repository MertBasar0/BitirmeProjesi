using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class MigTwelve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Sepetler",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sepetler_Musteriler_CustomerId",
                table: "Sepetler");

            migrationBuilder.DropIndex(
                name: "IX_Sepetler_CustomerId",
                table: "Sepetler");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Sepetler");
        }
    }
}
