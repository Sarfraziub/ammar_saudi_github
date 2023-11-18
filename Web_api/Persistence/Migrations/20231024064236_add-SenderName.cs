using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addSenderName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Gifts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "b319e882-46c0-42a2-af76-bb2486576f9f",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "1a28d5c4-e9d3-4a54-a1dc-0927fff57198");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Gifts");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "1a28d5c4-e9d3-4a54-a1dc-0927fff57198",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "b319e882-46c0-42a2-af76-bb2486576f9f");
        }
    }
}
