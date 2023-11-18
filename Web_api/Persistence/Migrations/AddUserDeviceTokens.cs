#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddUserDeviceTokens : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"UserDeviceTokens",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				UserId = table.Column<long>("bigint", nullable: false),
				DeviceType = table.Column<int>("int", nullable: false),
				Token = table.Column<string>("nvarchar(max)", nullable: true),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_UserDeviceTokens", x => x.Id);
				table.ForeignKey(
					"FK_UserDeviceTokens_AspNetUsers_UserId",
					x => x.UserId,
					"AspNetUsers",
					"Id",
					onDelete: ReferentialAction.Restrict);
			});

		migrationBuilder.CreateIndex(
			"IX_UserDeviceTokens_UserId",
			"UserDeviceTokens",
			"UserId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"UserDeviceTokens");
	}
}

