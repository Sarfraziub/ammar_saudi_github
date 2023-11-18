#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddPaymentTries : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"PaymentTries",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				ClientOrderId = table.Column<long>("bigint", nullable: false),
				ProfileId = table.Column<int>("int", nullable: false),
				TransactionType = table.Column<string>("nvarchar(max)", nullable: true),
				TransactionClass = table.Column<string>("nvarchar(max)", nullable: true),
				CartDescription = table.Column<string>("nvarchar(max)", nullable: true),
				OrderReferenceId = table.Column<string>("nvarchar(max)", nullable: true),
				Currency = table.Column<string>("nvarchar(max)", nullable: true),
				Amount = table.Column<float>("real", nullable: false),
				Return = table.Column<string>("nvarchar(max)", nullable: true),
				Callback = table.Column<string>("nvarchar(max)", nullable: true),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_PaymentTries", x => x.Id);
				table.ForeignKey(
					"FK_PaymentTries_ClientOrders_ClientOrderId",
					x => x.ClientOrderId,
					"ClientOrders",
					"Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			"IX_PaymentTries_ClientOrderId",
			"PaymentTries",
			"ClientOrderId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"PaymentTries");
	}
}

