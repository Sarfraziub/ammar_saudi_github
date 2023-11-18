using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddReceiptIdToClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReceiptId",
                table: "DriverClaims",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverClaims_ReceiptId",
                table: "DriverClaims",
                column: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverClaims_Files_ReceiptId",
                table: "DriverClaims",
                column: "ReceiptId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverClaims_Files_ReceiptId",
                table: "DriverClaims");

            migrationBuilder.DropIndex(
                name: "IX_DriverClaims_ReceiptId",
                table: "DriverClaims");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "DriverClaims");
        }
    }
}
