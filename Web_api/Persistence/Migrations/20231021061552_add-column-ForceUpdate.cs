using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addcolumnForceUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForceUpdate",
                table: "SiteConfigurations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "1a28d5c4-e9d3-4a54-a1dc-0927fff57198",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "e6cf9568-b017-4168-a347-5dd8c65b6f40");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForceUpdate",
                table: "SiteConfigurations");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "e6cf9568-b017-4168-a347-5dd8c65b6f40",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "1a28d5c4-e9d3-4a54-a1dc-0927fff57198");
        }
    }
}
