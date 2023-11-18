using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class add_flgSelected_clientOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "c8d0470e-b031-4086-87b3-0295518e65fa",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "0bd275a9-9c83-4bfb-b14f-f782a9955fff");

            migrationBuilder.AlterColumn<long>(
                name: "ImageId",
                table: "ContentSettings",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "FlgSelected",
                table: "ClientOrders",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlgSelected",
                table: "ClientOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "0bd275a9-9c83-4bfb-b14f-f782a9955fff",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "c8d0470e-b031-4086-87b3-0295518e65fa");

            migrationBuilder.AlterColumn<long>(
                name: "ImageId",
                table: "ContentSettings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
