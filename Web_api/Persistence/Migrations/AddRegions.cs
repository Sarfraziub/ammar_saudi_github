#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddRegions : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"Photos");

		migrationBuilder.CreateTable(
			"Regions",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>("varchar(120)", false, 120, nullable: false),
				ArabicName = table.Column<string>("nvarchar(120)", maxLength: 120, nullable: false),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table => { table.PrimaryKey("PK_Regions", x => x.Id); });
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"Regions");

		migrationBuilder.CreateTable(
			"Photos",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Active = table.Column<int>("int", nullable: false),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Description = table.Column<string>("nvarchar(max)", nullable: true),
				Name = table.Column<string>("nvarchar(max)", nullable: true),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true)
			},
			constraints: table => { table.PrimaryKey("PK_Photos", x => x.Id); });
	}
}

