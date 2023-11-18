#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddPaymentResponse : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"PaymentResponses",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				PaymentTryId = table.Column<long>("bigint", nullable: false),
				TransactionReference = table.Column<string>("nvarchar(max)", nullable: true),
				ResponseCode = table.Column<string>("nvarchar(max)", nullable: true),
				ResponseMessage = table.Column<string>("nvarchar(max)", nullable: true),
				ResponseStatus = table.Column<string>("nvarchar(max)", nullable: true),
				AcquirerMessage = table.Column<string>("nvarchar(max)", nullable: true),
				AcquirerRrn = table.Column<string>("nvarchar(max)", nullable: true),
				CartId = table.Column<string>("nvarchar(max)", nullable: true),
				CustomerEmail = table.Column<string>("nvarchar(max)", nullable: true),
				Signature = table.Column<string>("nvarchar(max)", nullable: true),
				Token = table.Column<string>("nvarchar(max)", nullable: true),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_PaymentResponses", x => x.Id);
				table.ForeignKey(
					"FK_PaymentResponses_PaymentTries_PaymentTryId",
					x => x.PaymentTryId,
					"PaymentTries",
					"Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			"IX_PaymentResponses_PaymentTryId",
			"PaymentResponses",
			"PaymentTryId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"PaymentResponses");
	}
}

