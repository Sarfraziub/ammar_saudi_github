#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddLocations : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"Locations",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				RegionId = table.Column<long>("bigint", nullable: false),
				Name = table.Column<string>("varchar(120)", false, 120, nullable: false),
				ArabicName = table.Column<string>("nvarchar(120)", maxLength: 120, nullable: false),
				Longitude = table.Column<double>("float", nullable: false),
				Latitude = table.Column<double>("float", nullable: false),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Locations", x => x.Id);
				table.ForeignKey(
					"FK_Locations_Regions_RegionId",
					x => x.RegionId,
					"Regions",
					"Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			"IX_Locations_RegionId",
			"Locations",
			"RegionId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"Locations");
	}
}

