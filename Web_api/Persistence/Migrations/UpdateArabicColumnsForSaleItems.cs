#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class UpdateArabicColumnsForSaleItems : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			"Specifications",
			"SaleItems",
			"varchar(500)",
			false,
			500,
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "nvarchar(max)",
			oldNullable: true);

		migrationBuilder.AlterColumn<string>(
			"Name",
			"SaleItems",
			"varchar(120)",
			false,
			120,
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "nvarchar(max)",
			oldNullable: true);

		migrationBuilder.AlterColumn<string>(
			"ArabicSpecifications",
			"SaleItems",
			"nvarchar(500)",
			maxLength: 500,
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "nvarchar(max)",
			oldNullable: true);

		migrationBuilder.AlterColumn<string>(
			"ArabicName",
			"SaleItems",
			"nvarchar(120)",
			maxLength: 120,
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "nvarchar(max)",
			oldNullable: true);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			"Specifications",
			"SaleItems",
			"nvarchar(max)",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "varchar(500)",
			oldUnicode: false,
			oldMaxLength: 500);

		migrationBuilder.AlterColumn<string>(
			"Name",
			"SaleItems",
			"nvarchar(max)",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "varchar(120)",
			oldUnicode: false,
			oldMaxLength: 120);

		migrationBuilder.AlterColumn<string>(
			"ArabicSpecifications",
			"SaleItems",
			"nvarchar(max)",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "nvarchar(500)",
			oldMaxLength: 500);

		migrationBuilder.AlterColumn<string>(
			"ArabicName",
			"SaleItems",
			"nvarchar(max)",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "nvarchar(120)",
			oldMaxLength: 120);
	}
}

