#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddClientOrderDetailsAndLogs : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<decimal>(
			"Price",
			"SaleItems",
			"money",
			nullable: false,
			oldClrType: typeof(double),
			oldType: "float");

		migrationBuilder.CreateTable(
			"ClientOrders",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Number = table.Column<string>("nvarchar(max)", nullable: true),
				ClientOrderStatus = table.Column<int>("int", nullable: false),
				DriverId = table.Column<long>("bigint", nullable: false),
				SaleItemId = table.Column<long>("bigint", nullable: true),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_ClientOrders", x => x.Id);
				table.ForeignKey(
					"FK_ClientOrders_AspNetUsers_DriverId",
					x => x.DriverId,
					"AspNetUsers",
					"Id",
					onDelete: ReferentialAction.Restrict);
				table.ForeignKey(
					"FK_ClientOrders_SaleItems_SaleItemId",
					x => x.SaleItemId,
					"SaleItems",
					"Id");
			});

		migrationBuilder.CreateTable(
			"ClientOrderDetails",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				ClientOrderId = table.Column<long>("bigint", nullable: false),
				SaleItemId = table.Column<long>("bigint", nullable: false),
				Price = table.Column<decimal>("money", nullable: false),
				Quantity = table.Column<int>("int", nullable: false),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_ClientOrderDetails", x => x.Id);
				table.ForeignKey(
					"FK_ClientOrderDetails_ClientOrders_ClientOrderId",
					x => x.ClientOrderId,
					"ClientOrders",
					"Id",
					onDelete: ReferentialAction.Restrict);
				table.ForeignKey(
					"FK_ClientOrderDetails_SaleItems_SaleItemId",
					x => x.SaleItemId,
					"SaleItems",
					"Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			"ClientOrderLogs",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				ClientOrderId = table.Column<long>("bigint", nullable: false),
				ClientOrderActionLog = table.Column<int>("int", nullable: false),
				Description = table.Column<string>("nvarchar(max)", nullable: true),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_ClientOrderLogs", x => x.Id);
				table.ForeignKey(
					"FK_ClientOrderLogs_ClientOrders_ClientOrderId",
					x => x.ClientOrderId,
					"ClientOrders",
					"Id",
					onDelete: ReferentialAction.Restrict);
			});

		migrationBuilder.CreateIndex(
			"IX_ClientOrderDetails_ClientOrderId",
			"ClientOrderDetails",
			"ClientOrderId");

		migrationBuilder.CreateIndex(
			"IX_ClientOrderDetails_SaleItemId",
			"ClientOrderDetails",
			"SaleItemId");

		migrationBuilder.CreateIndex(
			"IX_ClientOrderLogs_ClientOrderId",
			"ClientOrderLogs",
			"ClientOrderId");

		migrationBuilder.CreateIndex(
			"IX_ClientOrders_DriverId",
			"ClientOrders",
			"DriverId");

		migrationBuilder.CreateIndex(
			"IX_ClientOrders_SaleItemId",
			"ClientOrders",
			"SaleItemId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"ClientOrderDetails");

		migrationBuilder.DropTable(
			"ClientOrderLogs");

		migrationBuilder.DropTable(
			"ClientOrders");

		migrationBuilder.AlterColumn<double>(
			"Price",
			"SaleItems",
			"float",
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "money");
	}
}

