using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addtableSiteConfiguratins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "e6cf9568-b017-4168-a347-5dd8c65b6f40",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "dd76bdd8-4ef1-4e5c-b0d7-26cd82012d98");

            migrationBuilder.CreateTable(
                name: "SiteConfigurations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AndroidAppVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IosAppVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsMaintenanceMode = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteConfigurations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteConfigurations");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ContentSettings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "dd76bdd8-4ef1-4e5c-b0d7-26cd82012d98",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "e6cf9568-b017-4168-a347-5dd8c65b6f40");
        }
    }
}
