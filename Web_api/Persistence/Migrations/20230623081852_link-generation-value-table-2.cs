using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class linkgenerationvaluetable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkGeneration_AspNetUsers_UserId",
                table: "LinkGeneration");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkGenerationValue_LinkGeneration_LinkGenerationId",
                table: "LinkGenerationValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkGenerationValue",
                table: "LinkGenerationValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkGeneration",
                table: "LinkGeneration");

            migrationBuilder.RenameTable(
                name: "LinkGenerationValue",
                newName: "LinkGenerationValues");

            migrationBuilder.RenameTable(
                name: "LinkGeneration",
                newName: "LinkGenerations");

            migrationBuilder.RenameIndex(
                name: "IX_LinkGenerationValue_LinkGenerationId",
                table: "LinkGenerationValues",
                newName: "IX_LinkGenerationValues_LinkGenerationId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkGeneration_UserId",
                table: "LinkGenerations",
                newName: "IX_LinkGenerations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkGeneration_UniqueId",
                table: "LinkGenerations",
                newName: "IX_LinkGenerations_UniqueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkGenerationValues",
                table: "LinkGenerationValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkGenerations",
                table: "LinkGenerations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkGenerations_AspNetUsers_UserId",
                table: "LinkGenerations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkGenerationValues_LinkGenerations_LinkGenerationId",
                table: "LinkGenerationValues",
                column: "LinkGenerationId",
                principalTable: "LinkGenerations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkGenerations_AspNetUsers_UserId",
                table: "LinkGenerations");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkGenerationValues_LinkGenerations_LinkGenerationId",
                table: "LinkGenerationValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkGenerationValues",
                table: "LinkGenerationValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkGenerations",
                table: "LinkGenerations");

            migrationBuilder.RenameTable(
                name: "LinkGenerationValues",
                newName: "LinkGenerationValue");

            migrationBuilder.RenameTable(
                name: "LinkGenerations",
                newName: "LinkGeneration");

            migrationBuilder.RenameIndex(
                name: "IX_LinkGenerationValues_LinkGenerationId",
                table: "LinkGenerationValue",
                newName: "IX_LinkGenerationValue_LinkGenerationId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkGenerations_UserId",
                table: "LinkGeneration",
                newName: "IX_LinkGeneration_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkGenerations_UniqueId",
                table: "LinkGeneration",
                newName: "IX_LinkGeneration_UniqueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkGenerationValue",
                table: "LinkGenerationValue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkGeneration",
                table: "LinkGeneration",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkGeneration_AspNetUsers_UserId",
                table: "LinkGeneration",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkGenerationValue_LinkGeneration_LinkGenerationId",
                table: "LinkGenerationValue",
                column: "LinkGenerationId",
                principalTable: "LinkGeneration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
