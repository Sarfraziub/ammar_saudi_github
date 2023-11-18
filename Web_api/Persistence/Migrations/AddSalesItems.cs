#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddSalesItems : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"SaleItems",
			table => new
			{
				Id = table.Column<long>("bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>("nvarchar(max)", nullable: true),
				ArabicName = table.Column<string>("nvarchar(max)", nullable: true),
				Specifications = table.Column<string>("nvarchar(max)", nullable: true),
				ArabicSpecifications = table.Column<string>("nvarchar(max)", nullable: true),
				Price = table.Column<double>("float", nullable: false),
				ImageId = table.Column<long>("bigint", nullable: false),
				Created = table.Column<DateTime>("datetime", nullable: false),
				CreatedBy = table.Column<string>("nvarchar(200)", nullable: false),
				Updated = table.Column<DateTime>("datetime", nullable: true),
				UpdatedBy = table.Column<string>("nvarchar(200)", nullable: true),
				Active = table.Column<int>("int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_SaleItems", x => x.Id);
				table.ForeignKey(
					"FK_SaleItems_Files_ImageId",
					x => x.ImageId,
					"Files",
					"Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			"IX_SaleItems_ImageId",
			"SaleItems",
			"ImageId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"SaleItems");
	}
}

