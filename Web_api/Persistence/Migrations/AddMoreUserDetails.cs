#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddMoreUserDetails : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<int>(
			"PaymentType",
			"PaymentTries",
			"int",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.AddColumn<string>(
			"City",
			"AspNetUsers",
			"nvarchar(max)",
			nullable: true);

		migrationBuilder.AddColumn<int>(
			"Language",
			"AspNetUsers",
			"int",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.AddColumn<string>(
			"State",
			"AspNetUsers",
			"nvarchar(max)",
			nullable: true);

		migrationBuilder.AddColumn<string>(
			"Street",
			"AspNetUsers",
			"nvarchar(max)",
			nullable: true);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			"PaymentType",
			"PaymentTries");

		migrationBuilder.DropColumn(
			"City",
			"AspNetUsers");

		migrationBuilder.DropColumn(
			"Language",
			"AspNetUsers");

		migrationBuilder.DropColumn(
			"State",
			"AspNetUsers");

		migrationBuilder.DropColumn(
			"Street",
			"AspNetUsers");
	}
}

