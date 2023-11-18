#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddBankNameDriverUsers1 : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			"FK_PaymentTries_ClientOrders_ClientOrderId1",
			"PaymentTries");

		migrationBuilder.DropIndex(
			"IX_PaymentTries_ClientOrderId1",
			"PaymentTries");

		migrationBuilder.DropColumn(
			"ClientOrderId1",
			"PaymentTries");

		migrationBuilder.AlterColumn<long>(
			"ClientOrderId",
			"PaymentTries",
			"bigint",
			nullable: false,
			defaultValue: 0L,
			oldClrType: typeof(string),
			oldType: "nvarchar(max)",
			oldNullable: true);

		migrationBuilder.AddColumn<string>(
			"BankName",
			"AspNetUsers",
			"nvarchar(max)",
			nullable: true);

		migrationBuilder.CreateIndex(
			"IX_PaymentTries_ClientOrderId",
			"PaymentTries",
			"ClientOrderId");

		migrationBuilder.AddForeignKey(
			"FK_PaymentTries_ClientOrders_ClientOrderId",
			"PaymentTries",
			"ClientOrderId",
			"ClientOrders",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			"FK_PaymentTries_ClientOrders_ClientOrderId",
			"PaymentTries");

		migrationBuilder.DropIndex(
			"IX_PaymentTries_ClientOrderId",
			"PaymentTries");

		migrationBuilder.DropColumn(
			"BankName",
			"AspNetUsers");

		migrationBuilder.AlterColumn<string>(
			"ClientOrderId",
			"PaymentTries",
			"nvarchar(max)",
			nullable: true,
			oldClrType: typeof(long),
			oldType: "bigint");

		migrationBuilder.AddColumn<long>(
			"ClientOrderId1",
			"PaymentTries",
			"bigint",
			nullable: true);

		migrationBuilder.CreateIndex(
			"IX_PaymentTries_ClientOrderId1",
			"PaymentTries",
			"ClientOrderId1");

		migrationBuilder.AddForeignKey(
			"FK_PaymentTries_ClientOrders_ClientOrderId1",
			"PaymentTries",
			"ClientOrderId1",
			"ClientOrders",
			principalColumn: "Id");
	}
}

