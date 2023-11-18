#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class UpdatePaymentTriesForRequestPage : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			"CartId",
			"PaymentTries",
			"nvarchar(max)",
			nullable: true);

		migrationBuilder.AddColumn<string>(
			"Message",
			"PaymentTries",
			"nvarchar(max)",
			nullable: true);

		migrationBuilder.AddColumn<string>(
			"RedirectUrl",
			"PaymentTries",
			"nvarchar(max)",
			nullable: true);

		migrationBuilder.AddColumn<string>(
			"Trace",
			"PaymentTries",
			"nvarchar(max)",
			nullable: true);

		migrationBuilder.AddColumn<string>(
			"TransactionReference",
			"PaymentTries",
			"nvarchar(max)",
			nullable: true);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			"CartId",
			"PaymentTries");

		migrationBuilder.DropColumn(
			"Message",
			"PaymentTries");

		migrationBuilder.DropColumn(
			"RedirectUrl",
			"PaymentTries");

		migrationBuilder.DropColumn(
			"Trace",
			"PaymentTries");

		migrationBuilder.DropColumn(
			"TransactionReference",
			"PaymentTries");
	}
}

