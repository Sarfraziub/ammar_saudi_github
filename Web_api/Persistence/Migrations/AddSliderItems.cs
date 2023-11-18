#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddSliderItems : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"SliderItems",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>("nvarchar(max)", nullable: true),
				ImageId = table.Column<long>("bigint", nullable: false),
				Visible = table.Column<bool>("bit", nullable: false),
				Order = table.Column<int>("int", nullable: false),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_SliderItems", x => x.Id);
				table.ForeignKey(
					"FK_SliderItems_Files_ImageId",
					x => x.ImageId,
					"Files",
					"Id",
					onDelete: ReferentialAction.Restrict);
			});

		migrationBuilder.CreateIndex(
			"IX_SliderItems_ImageId",
			"SliderItems",
			"ImageId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"SliderItems");
	}
}

