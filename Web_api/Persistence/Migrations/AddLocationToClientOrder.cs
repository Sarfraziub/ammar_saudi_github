#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddLocationToClientOrder : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<long>(
			"LocationId",
			"ClientOrders",
			"bigint",
			nullable: true);

		migrationBuilder.CreateIndex(
			"IX_ClientOrders_LocationId",
			"ClientOrders",
			"LocationId");

		migrationBuilder.AddForeignKey(
			"FK_ClientOrders_Locations_LocationId",
			"ClientOrders",
			"LocationId",
			"Locations",
			principalColumn: "Id",
			onDelete: ReferentialAction.Restrict);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			"FK_ClientOrders_Locations_LocationId",
			"ClientOrders");

		migrationBuilder.DropIndex(
			"IX_ClientOrders_LocationId",
			"ClientOrders");

		migrationBuilder.DropColumn(
			"LocationId",
			"ClientOrders");
	}
}

