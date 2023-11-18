using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class nullableproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentTries",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PaymentTries",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

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
    }
}
