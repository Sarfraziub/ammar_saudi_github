#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddCountry : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"Countries",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Abbreviation = table.Column<string>("nvarchar(max)", nullable: true),
				Name = table.Column<string>("nvarchar(max)", nullable: true),
				ArabicName = table.Column<string>("nvarchar(max)", nullable: true),
				Created = table.Column<DateTime>("datetime2", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
				Updated = table.Column<DateTime>("datetime2", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table => { table.PrimaryKey("PK_Countries", x => x.Id); });
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"Countries");
	}
}

