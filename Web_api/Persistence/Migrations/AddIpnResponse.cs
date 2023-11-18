#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddIpnResponse : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"IpnResponses",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				PaymentTryId = table.Column<long>("bigint", nullable: false),
				Response = table.Column<string>("nvarchar(max)", nullable: true),
				Created = table.Column<DateTime>("datetime2", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
				Updated = table.Column<DateTime>("datetime2", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_IpnResponses", x => x.Id);
				table.ForeignKey(
					"FK_IpnResponses_PaymentTries_PaymentTryId",
					x => x.PaymentTryId,
					"PaymentTries",
					"Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			"IX_IpnResponses_PaymentTryId",
			"IpnResponses",
			"PaymentTryId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"IpnResponses");
	}
}

