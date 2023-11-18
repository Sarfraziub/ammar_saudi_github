using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addkeyCOntentSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "dd76bdd8-4ef1-4e5c-b0d7-26cd82012d98");

            migrationBuilder.CreateIndex(
                name: "IX_ContentSettings_Key",
                table: "ContentSettings",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContentSettings_Key",
                table: "ContentSettings");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "ContentSettings");
        }
    }
}
