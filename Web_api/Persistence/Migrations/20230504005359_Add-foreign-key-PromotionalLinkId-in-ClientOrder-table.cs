using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddforeignkeyPromotionalLinkIdinClientOrdertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PromotionalLinkId",
                table: "ClientOrders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientOrders_PromotionalLinkId",
                table: "ClientOrders",
                column: "PromotionalLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientOrders_PromotionalLinks_PromotionalLinkId",
                table: "ClientOrders",
                column: "PromotionalLinkId",
                principalTable: "PromotionalLinks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientOrders_PromotionalLinks_PromotionalLinkId",
                table: "ClientOrders");

            migrationBuilder.DropIndex(
                name: "IX_ClientOrders_PromotionalLinkId",
                table: "ClientOrders");

            migrationBuilder.DropColumn(
                name: "PromotionalLinkId",
                table: "ClientOrders");
        }
    }
}
