using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class IsUniqueForSaleItemIdAndClientOrderIdRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientOrderDetails_SaleItemId_ClientOrderId",
                table: "ClientOrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrderDetails_SaleItemId",
                table: "ClientOrderDetails",
                column: "SaleItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientOrderDetails_SaleItemId",
                table: "ClientOrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrderDetails_SaleItemId_ClientOrderId",
                table: "ClientOrderDetails",
                columns: new[] { "SaleItemId", "ClientOrderId" },
                unique: true);
        }
    }
}
