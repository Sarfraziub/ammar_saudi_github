#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class ChangeColumnNameForClientOrders : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			"ClientOrderActionLog",
			"ClientOrderLogs",
			"ClientOrderActionLogStatus");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			"ClientOrderActionLogStatus",
			"ClientOrderLogs",
			"ClientOrderActionLog");
	}
}

