#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddPromoCodes : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<long>(
			"PromoCodeId",
			"ClientOrders",
			"bigint",
			nullable: true);

		migrationBuilder.CreateTable(
			"PromoCodes",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Code = table.Column<string>("nvarchar(max)", nullable: true),
				Expiry = table.Column<DateTime>("datetime2", nullable: false),
				Percentage = table.Column<decimal>("decimal(18,2)", nullable: false),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table => { table.PrimaryKey("PK_PromoCodes", x => x.Id); });

		migrationBuilder.CreateIndex(
			"IX_ClientOrders_PromoCodeId",
			"ClientOrders",
			"PromoCodeId");

		migrationBuilder.AddForeignKey(
			"FK_ClientOrders_PromoCodes_PromoCodeId",
			"ClientOrders",
			"PromoCodeId",
			"PromoCodes",
			principalColumn: "Id");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			"FK_ClientOrders_PromoCodes_PromoCodeId",
			"ClientOrders");

		migrationBuilder.DropTable(
			"PromoCodes");

		migrationBuilder.DropIndex(
			"IX_ClientOrders_PromoCodeId",
			"ClientOrders");

		migrationBuilder.DropColumn(
			"PromoCodeId",
			"ClientOrders");
	}
}

