#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddMoreDetailsForDrivers : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<long>(
			"IbanImageId",
			"AspNetUsers",
			"bigint",
			nullable: true);

		migrationBuilder.AddColumn<long>(
			"NationalImageImageId",
			"AspNetUsers",
			"bigint",
			nullable: true);

		migrationBuilder.CreateIndex(
			"IX_AspNetUsers_IbanImageId",
			"AspNetUsers",
			"IbanImageId");

		migrationBuilder.CreateIndex(
			"IX_AspNetUsers_NationalImageImageId",
			"AspNetUsers",
			"NationalImageImageId");

		migrationBuilder.AddForeignKey(
			"FK_AspNetUsers_Files_IbanImageId",
			"AspNetUsers",
			"IbanImageId",
			"Files",
			principalColumn: "Id",
			onDelete: ReferentialAction.Restrict);

		migrationBuilder.AddForeignKey(
			"FK_AspNetUsers_Files_NationalImageImageId",
			"AspNetUsers",
			"NationalImageImageId",
			"Files",
			principalColumn: "Id",
			onDelete: ReferentialAction.Restrict);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			"FK_AspNetUsers_Files_IbanImageId",
			"AspNetUsers");

		migrationBuilder.DropForeignKey(
			"FK_AspNetUsers_Files_NationalImageImageId",
			"AspNetUsers");

		migrationBuilder.DropIndex(
			"IX_AspNetUsers_IbanImageId",
			"AspNetUsers");

		migrationBuilder.DropIndex(
			"IX_AspNetUsers_NationalImageImageId",
			"AspNetUsers");

		migrationBuilder.DropColumn(
			"IbanImageId",
			"AspNetUsers");

		migrationBuilder.DropColumn(
			"NationalImageImageId",
			"AspNetUsers");
	}
}

