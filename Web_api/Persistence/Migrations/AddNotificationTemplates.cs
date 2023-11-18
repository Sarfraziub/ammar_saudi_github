#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddNotificationTemplates : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			"Token",
			"UserDeviceTokens",
			"RegistrationToken");

		migrationBuilder.CreateTable(
			"NotificationTemplates",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Title = table.Column<string>("nvarchar(max)", nullable: true),
				Body = table.Column<string>("nvarchar(max)", nullable: true),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table => { table.PrimaryKey("PK_NotificationTemplates", x => x.Id); });
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"NotificationTemplates");

		migrationBuilder.RenameColumn(
			"RegistrationToken",
			"UserDeviceTokens",
			"Token");
	}
}

