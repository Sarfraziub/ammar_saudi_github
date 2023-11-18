using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class makeuniqueidunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "PromotionalLinks",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 120);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionalLinks_UniqueId",
                table: "PromotionalLinks",
                column: "UniqueId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PromotionalLinks_UniqueId",
                table: "PromotionalLinks");

            migrationBuilder.AlterColumn<Guid>(
                name: "UniqueId",
                table: "PromotionalLinks",
                type: "uniqueidentifier",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);
        }
    }
}
