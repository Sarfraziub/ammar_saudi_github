using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class RemovePaymentServiceFromBackend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientOrders_DriverClaim_DriverClaimId",
                table: "ClientOrders");

            migrationBuilder.DropTable(
                name: "IpnResponses");

            migrationBuilder.DropTable(
                name: "PaymentResponses");

            migrationBuilder.DropTable(
                name: "PaymentTries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverClaim",
                table: "DriverClaim");

            migrationBuilder.RenameTable(
                name: "DriverClaim",
                newName: "DriverClaims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverClaims",
                table: "DriverClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientOrders_DriverClaims_DriverClaimId",
                table: "ClientOrders",
                column: "DriverClaimId",
                principalTable: "DriverClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientOrders_DriverClaims_DriverClaimId",
                table: "ClientOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverClaims",
                table: "DriverClaims");

            migrationBuilder.RenameTable(
                name: "DriverClaims",
                newName: "DriverClaim");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverClaim",
                table: "DriverClaim",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PaymentTries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientOrderId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Callback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    RedirectUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Return = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTries_ClientOrders_ClientOrderId",
                        column: x => x.ClientOrderId,
                        principalTable: "ClientOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IpnResponses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTryId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpnResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IpnResponses_PaymentTries_PaymentTryId",
                        column: x => x.PaymentTryId,
                        principalTable: "PaymentTries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentResponses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTryId = table.Column<long>(type: "bigint", nullable: false),
                    AcquirerMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcquirerRrn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentResponses_PaymentTries_PaymentTryId",
                        column: x => x.PaymentTryId,
                        principalTable: "PaymentTries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IpnResponses_PaymentTryId",
                table: "IpnResponses",
                column: "PaymentTryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentResponses_PaymentTryId",
                table: "PaymentResponses",
                column: "PaymentTryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTries_ClientOrderId",
                table: "PaymentTries",
                column: "ClientOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientOrders_DriverClaim_DriverClaimId",
                table: "ClientOrders",
                column: "DriverClaimId",
                principalTable: "DriverClaim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
