using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class MigSixteen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsInStock",
                table: "Urunler");

            migrationBuilder.AddColumn<DateTime>(
                name: "OluşturmaTarihi",
                table: "Satişlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OluşturmaTarihi",
                table: "Satişlar");

            migrationBuilder.AddColumn<int>(
                name: "UnitsInStock",
                table: "Urunler",
                type: "int",
                nullable: true);
        }
    }
}
