#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AdjustPriceForSaleItem : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			"Active",
			"AspNetUsers");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<int>(
			"Active",
			"AspNetUsers",
			"int",
			nullable: false,
			defaultValue: 0);
	}
}

