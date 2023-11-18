#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddNationalIdToDriverUsers : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			"NationalId",
			"AspNetUsers",
			"nvarchar(max)",
			nullable: true);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			"NationalId",
			"AspNetUsers");
	}
}

