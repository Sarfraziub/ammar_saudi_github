#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddDescriptionAndArabicDescriptionForLocation : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			"ArabicDescription",
			"Locations",
			"nvarchar(200)",
			maxLength: 200,
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<string>(
			"Description",
			"Locations",
			"varchar(200)",
			false,
			200,
			nullable: false,
			defaultValue: "");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			"ArabicDescription",
			"Locations");

		migrationBuilder.DropColumn(
			"Description",
			"Locations");
	}
}

