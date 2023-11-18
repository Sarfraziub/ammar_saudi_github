using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class added_imageId_to_contentSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "0bd275a9-9c83-4bfb-b14f-f782a9955fff",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "b319e882-46c0-42a2-af76-bb2486576f9f");

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "ContentSettings",
                type: "bigint",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ContentSettings_ImageId",
                table: "ContentSettings",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentSettings_Files_ImageId",
                table: "ContentSettings",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentSettings_Files_ImageId",
                table: "ContentSettings");

            migrationBuilder.DropIndex(
                name: "IX_ContentSettings_ImageId",
                table: "ContentSettings");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ContentSettings");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "b319e882-46c0-42a2-af76-bb2486576f9f",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "0bd275a9-9c83-4bfb-b14f-f782a9955fff");
        }
    }
}
