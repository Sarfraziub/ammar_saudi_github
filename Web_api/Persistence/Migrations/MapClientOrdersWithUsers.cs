#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class MapClientOrdersWithUsers : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<long>(
			"ClientId",
			"ClientOrders",
			"bigint",
			nullable: false,
			defaultValue: 0L);

		migrationBuilder.CreateIndex(
			"IX_ClientOrders_ClientId",
			"ClientOrders",
			"ClientId");

		migrationBuilder.AddForeignKey(
			"FK_ClientOrders_AspNetUsers_ClientId",
			"ClientOrders",
			"ClientId",
			"AspNetUsers",
			principalColumn: "Id",
			onDelete: ReferentialAction.Restrict);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			"FK_ClientOrders_AspNetUsers_ClientId",
			"ClientOrders");

		migrationBuilder.DropIndex(
			"IX_ClientOrders_ClientId",
			"ClientOrders");

		migrationBuilder.DropColumn(
			"ClientId",
			"ClientOrders");
	}
}

