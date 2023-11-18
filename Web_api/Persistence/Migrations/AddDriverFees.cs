using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddDriverFees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DriverFeeId",
                table: "ClientOrders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DriverFees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Active = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverFees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrders_DriverFeeId",
                table: "ClientOrders",
                column: "DriverFeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientOrders_DriverFees_DriverFeeId",
                table: "ClientOrders",
                column: "DriverFeeId",
                principalTable: "DriverFees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientOrders_DriverFees_DriverFeeId",
                table: "ClientOrders");

            migrationBuilder.DropTable(
                name: "DriverFees");

            migrationBuilder.DropIndex(
                name: "IX_ClientOrders_DriverFeeId",
                table: "ClientOrders");

            migrationBuilder.DropColumn(
                name: "DriverFeeId",
                table: "ClientOrders");
        }
    }
}
