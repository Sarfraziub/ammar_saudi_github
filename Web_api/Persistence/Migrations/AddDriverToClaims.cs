using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddDriverToClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DriverId",
                table: "DriverClaims",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverClaims_DriverId",
                table: "DriverClaims",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverClaims_AspNetUsers_DriverId",
                table: "DriverClaims",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverClaims_AspNetUsers_DriverId",
                table: "DriverClaims");

            migrationBuilder.DropIndex(
                name: "IX_DriverClaims_DriverId",
                table: "DriverClaims");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "DriverClaims");
        }
    }
}
