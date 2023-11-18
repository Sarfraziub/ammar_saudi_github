#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AdjustDriverInClientOrder : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<long>(
			"DriverId",
			"ClientOrders",
			"bigint",
			nullable: true,
			oldClrType: typeof(long),
			oldType: "bigint");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<long>(
			"DriverId",
			"ClientOrders",
			"bigint",
			nullable: false,
			defaultValue: 0L,
			oldClrType: typeof(long),
			oldType: "bigint",
			oldNullable: true);
	}
}

