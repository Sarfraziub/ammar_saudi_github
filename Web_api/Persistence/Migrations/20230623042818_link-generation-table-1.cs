using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class linkgenerationtable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkGeneration_AspNetUsers_UserId1",
                table: "LinkGeneration");

            migrationBuilder.DropIndex(
                name: "IX_LinkGeneration_UserId1",
                table: "LinkGeneration");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "LinkGeneration");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "LinkGeneration",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_LinkGeneration_UserId",
                table: "LinkGeneration",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkGeneration_AspNetUsers_UserId",
                table: "LinkGeneration",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkGeneration_AspNetUsers_UserId",
                table: "LinkGeneration");

            migrationBuilder.DropIndex(
                name: "IX_LinkGeneration_UserId",
                table: "LinkGeneration");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LinkGeneration",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "LinkGeneration",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LinkGeneration_UserId1",
                table: "LinkGeneration",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkGeneration_AspNetUsers_UserId1",
                table: "LinkGeneration",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
