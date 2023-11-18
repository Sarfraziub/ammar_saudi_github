using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addFkwithcurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ChargedCurrencyId",
                table: "ClientOrders",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrders_ChargedCurrencyId",
                table: "ClientOrders",
                column: "ChargedCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientOrders_Currency_ChargedCurrencyId",
                table: "ClientOrders",
                column: "ChargedCurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientOrders_Currency_ChargedCurrencyId",
                table: "ClientOrders");

            migrationBuilder.DropIndex(
                name: "IX_ClientOrders_ChargedCurrencyId",
                table: "ClientOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ChargedCurrencyId",
                table: "ClientOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
