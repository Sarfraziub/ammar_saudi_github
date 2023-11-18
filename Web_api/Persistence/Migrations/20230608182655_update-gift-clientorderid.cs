using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class updategiftclientorderid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_ClientOrders_ClientOrderId1",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_ClientOrderId1",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "ClientOrderId1",
                table: "Gifts");

            migrationBuilder.AlterColumn<long>(
                name: "ClientOrderId",
                table: "Gifts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_ClientOrderId",
                table: "Gifts",
                column: "ClientOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_ClientOrders_ClientOrderId",
                table: "Gifts",
                column: "ClientOrderId",
                principalTable: "ClientOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_ClientOrders_ClientOrderId",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_ClientOrderId",
                table: "Gifts");

            migrationBuilder.AlterColumn<int>(
                name: "ClientOrderId",
                table: "Gifts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ClientOrderId1",
                table: "Gifts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_ClientOrderId1",
                table: "Gifts",
                column: "ClientOrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_ClientOrders_ClientOrderId1",
                table: "Gifts",
                column: "ClientOrderId1",
                principalTable: "ClientOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
