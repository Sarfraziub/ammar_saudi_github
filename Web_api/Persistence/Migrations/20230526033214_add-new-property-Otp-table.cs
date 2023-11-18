using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addnewpropertyOtptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otps_AspNetUsers_UserId",
                table: "Otps");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Otps",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Otps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Otps_AspNetUsers_UserId",
                table: "Otps",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otps_AspNetUsers_UserId",
                table: "Otps");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Otps");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Otps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Otps_AspNetUsers_UserId",
                table: "Otps",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
