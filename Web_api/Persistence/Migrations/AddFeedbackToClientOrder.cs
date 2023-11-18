#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddFeedbackToClientOrder : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			"Feedback",
			"ClientOrders",
			"nvarchar(max)",
			nullable: true);

		migrationBuilder.AddColumn<int>(
			"Rate",
			"ClientOrders",
			"int",
			nullable: true);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			"Feedback",
			"ClientOrders");

		migrationBuilder.DropColumn(
			"Rate",
			"ClientOrders");
	}
}

