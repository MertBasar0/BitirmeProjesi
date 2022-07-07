using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class MigSix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Products_ProductId",
                table: "BasketProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts");

            migrationBuilder.RenameTable(
                name: "Sales",
                newName: "Satişlar");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Urunler");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Musteriler");

            migrationBuilder.RenameTable(
                name: "Baskets",
                newName: "Sepetler");

            migrationBuilder.RenameTable(
                name: "BasketProducts",
                newName: "SepetUrunEslesme");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_CustomerId",
                table: "Satişlar",
                newName: "IX_Satişlar_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_BasketId",
                table: "SepetUrunEslesme",
                newName: "IX_SepetUrunEslesme_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Satişlar",
                table: "Satişlar",
                column: "SaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Urunler",
                table: "Urunler",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sepetler",
                table: "Sepetler",
                column: "BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SepetUrunEslesme",
                table: "SepetUrunEslesme",
                columns: new[] { "ProductId", "BasketId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Satişlar_Musteriler_CustomerId",
                table: "Satişlar",
                column: "CustomerId",
                principalTable: "Musteriler",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SepetUrunEslesme_Sepetler_BasketId",
                table: "SepetUrunEslesme",
                column: "BasketId",
                principalTable: "Sepetler",
                principalColumn: "BasketId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SepetUrunEslesme_Urunler_ProductId",
                table: "SepetUrunEslesme",
                column: "ProductId",
                principalTable: "Urunler",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Satişlar_Musteriler_CustomerId",
                table: "Satişlar");

            migrationBuilder.DropForeignKey(
                name: "FK_SepetUrunEslesme_Sepetler_BasketId",
                table: "SepetUrunEslesme");

            migrationBuilder.DropForeignKey(
                name: "FK_SepetUrunEslesme_Urunler_ProductId",
                table: "SepetUrunEslesme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Urunler",
                table: "Urunler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SepetUrunEslesme",
                table: "SepetUrunEslesme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sepetler",
                table: "Sepetler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Satişlar",
                table: "Satişlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler");

            migrationBuilder.RenameTable(
                name: "Urunler",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "SepetUrunEslesme",
                newName: "BasketProducts");

            migrationBuilder.RenameTable(
                name: "Sepetler",
                newName: "Baskets");

            migrationBuilder.RenameTable(
                name: "Satişlar",
                newName: "Sales");

            migrationBuilder.RenameTable(
                name: "Musteriler",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_SepetUrunEslesme_BasketId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_Satişlar_CustomerId",
                table: "Sales",
                newName: "IX_Sales_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts",
                columns: new[] { "ProductId", "BasketId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets",
                column: "BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                column: "SaleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "BasketId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Products_ProductId",
                table: "BasketProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
