using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class updateExchangeRatetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_FromCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_ToCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_FromCurrencyId",
                table: "ExchangeRates",
                column: "FromCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_ToCurrencyId",
                table: "ExchangeRates",
                column: "ToCurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_FromCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_ToCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_FromCurrencyId",
                table: "ExchangeRates",
                column: "FromCurrencyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_ToCurrencyId",
                table: "ExchangeRates",
                column: "ToCurrencyId",
                unique: true);
        }
    }
}
