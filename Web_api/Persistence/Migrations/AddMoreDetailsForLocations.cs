#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddMoreDetailsForLocations : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<int>(
			"LocationType",
			"Locations",
			"int",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.AddColumn<bool>(
			"MoreNeeded",
			"Locations",
			"bit",
			nullable: false,
			defaultValue: false);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			"LocationType",
			"Locations");

		migrationBuilder.DropColumn(
			"MoreNeeded",
			"Locations");
	}
}

