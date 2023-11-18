#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddImageToUserProfile : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<long>(
			"ImageId",
			"AspNetUsers",
			"bigint",
			nullable: true);

		migrationBuilder.CreateIndex(
			"IX_AspNetUsers_ImageId",
			"AspNetUsers",
			"ImageId");

		migrationBuilder.AddForeignKey(
			"FK_AspNetUsers_Files_ImageId",
			"AspNetUsers",
			"ImageId",
			"Files",
			principalColumn: "Id",
			onDelete: ReferentialAction.Restrict);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			"FK_AspNetUsers_Files_ImageId",
			"AspNetUsers");

		migrationBuilder.DropIndex(
			"IX_AspNetUsers_ImageId",
			"AspNetUsers");

		migrationBuilder.DropColumn(
			"ImageId",
			"AspNetUsers");
	}
}

