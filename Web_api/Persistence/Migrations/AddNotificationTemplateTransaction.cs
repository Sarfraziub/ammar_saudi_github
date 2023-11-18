#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddNotificationTemplateTransaction : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"NotificationTemplateTransactions",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				NotificationTemplateId = table.Column<long>("bigint", nullable: false),
				Response = table.Column<string>("nvarchar(max)", nullable: true),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_NotificationTemplateTransactions", x => x.Id);
				table.ForeignKey(
					"FK_NotificationTemplateTransactions_NotificationTemplates_NotificationTemplateId",
					x => x.NotificationTemplateId,
					"NotificationTemplates",
					"Id",
					onDelete: ReferentialAction.Restrict);
			});

		migrationBuilder.CreateIndex(
			"IX_NotificationTemplateTransactions_NotificationTemplateId",
			"NotificationTemplateTransactions",
			"NotificationTemplateId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"NotificationTemplateTransactions");
	}
}

