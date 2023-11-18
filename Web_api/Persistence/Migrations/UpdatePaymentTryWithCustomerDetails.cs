using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class UpdatePaymentTryWithCustomerDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PaymentTries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "PaymentTries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "PaymentTries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "PaymentTries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "PaymentTries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "PaymentTries",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "PaymentTries");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "PaymentTries");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "PaymentTries");

            migrationBuilder.DropColumn(
                name: "State",
                table: "PaymentTries");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "PaymentTries");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "PaymentTries");
        }
    }
}
