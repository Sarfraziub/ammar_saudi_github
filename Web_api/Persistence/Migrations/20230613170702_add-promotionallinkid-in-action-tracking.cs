using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addpromotionallinkidinactiontracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "ActionTrackingHistories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "PromotionalLinkId",
                table: "ActionTrackingHistories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActionTrackingHistories_PromotionalLinkId",
                table: "ActionTrackingHistories",
                column: "PromotionalLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionTrackingHistories_UserId",
                table: "ActionTrackingHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTrackingHistories_AspNetUsers_UserId",
                table: "ActionTrackingHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTrackingHistories_PromotionalLinks_PromotionalLinkId",
                table: "ActionTrackingHistories",
                column: "PromotionalLinkId",
                principalTable: "PromotionalLinks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionTrackingHistories_AspNetUsers_UserId",
                table: "ActionTrackingHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ActionTrackingHistories_PromotionalLinks_PromotionalLinkId",
                table: "ActionTrackingHistories");

            migrationBuilder.DropIndex(
                name: "IX_ActionTrackingHistories_PromotionalLinkId",
                table: "ActionTrackingHistories");

            migrationBuilder.DropIndex(
                name: "IX_ActionTrackingHistories_UserId",
                table: "ActionTrackingHistories");

            migrationBuilder.DropColumn(
                name: "PromotionalLinkId",
                table: "ActionTrackingHistories");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ActionTrackingHistories",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
