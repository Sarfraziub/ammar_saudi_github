#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AdjustClientOrderLogToAllowNullForCreatedBy : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			"UpdatedBy",
			"ClientOrderLogs",
			"nvarchar(max)",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "nvarchar(200)",
			oldNullable: true);

		migrationBuilder.AlterColumn<DateTime>(
			"Updated",
			"ClientOrderLogs",
			"datetime2",
			nullable: true,
			oldClrType: typeof(DateTime),
			oldType: "datetime",
			oldNullable: true);

		migrationBuilder.AlterColumn<string>(
			"CreatedBy",
			"ClientOrderLogs",
			"nvarchar(max)",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "nvarchar(200)");

		migrationBuilder.AlterColumn<DateTime>(
			"Created",
			"ClientOrderLogs",
			"datetime2",
			nullable: false,
			oldClrType: typeof(DateTime),
			oldType: "datetime");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			"UpdatedBy",
			"ClientOrderLogs",
			"nvarchar(200)",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "nvarchar(max)",
			oldNullable: true);

		migrationBuilder.AlterColumn<DateTime>(
			"Updated",
			"ClientOrderLogs",
			"datetime",
			nullable: true,
			oldClrType: typeof(DateTime),
			oldType: "datetime2",
			oldNullable: true);

		migrationBuilder.AlterColumn<string>(
			"CreatedBy",
			"ClientOrderLogs",
			"nvarchar(200)",
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "nvarchar(max)",
			oldNullable: true);

		migrationBuilder.AlterColumn<DateTime>(
			"Created",
			"ClientOrderLogs",
			"datetime",
			nullable: false,
			oldClrType: typeof(DateTime),
			oldType: "datetime2");
	}
}

