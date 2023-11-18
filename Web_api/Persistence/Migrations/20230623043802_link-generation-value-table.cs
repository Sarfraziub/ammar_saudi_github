using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class linkgenerationvaluetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkGenerationValue",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LinkGenerationId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkGenerationValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkGenerationValue_LinkGeneration_LinkGenerationId",
                        column: x => x.LinkGenerationId,
                        principalTable: "LinkGeneration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkGeneration_UniqueId",
                table: "LinkGeneration",
                column: "UniqueId",
                unique: true,
                filter: "[UniqueId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LinkGenerationValue_LinkGenerationId",
                table: "LinkGenerationValue",
                column: "LinkGenerationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkGenerationValue");

            migrationBuilder.DropIndex(
                name: "IX_LinkGeneration_UniqueId",
                table: "LinkGeneration");
        }
    }
}
