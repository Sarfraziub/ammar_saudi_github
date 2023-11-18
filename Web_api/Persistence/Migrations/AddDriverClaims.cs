using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddDriverClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DriverClaimId",
                table: "ClientOrders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DriverClaim",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverClaimNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverClaimStatus = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Active = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverClaim", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrders_DriverClaimId",
                table: "ClientOrders",
                column: "DriverClaimId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientOrders_DriverClaim_DriverClaimId",
                table: "ClientOrders",
                column: "DriverClaimId",
                principalTable: "DriverClaim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientOrders_DriverClaim_DriverClaimId",
                table: "ClientOrders");

            migrationBuilder.DropTable(
                name: "DriverClaim");

            migrationBuilder.DropIndex(
                name: "IX_ClientOrders_DriverClaimId",
                table: "ClientOrders");

            migrationBuilder.DropColumn(
                name: "DriverClaimId",
                table: "ClientOrders");
        }
    }
}
