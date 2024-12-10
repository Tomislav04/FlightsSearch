using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightSearchRequests",
                columns: table => new
                {
                    FlightSearchRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Passengers = table.Column<int>(type: "int", nullable: false),
                    AdultsPassengers = table.Column<int>(type: "int", nullable: false),
                    KidsPassengers = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSearchRequests", x => x.FlightSearchRequestId);
                });

            migrationBuilder.CreateTable(
                name: "FlightSearchResults",
                columns: table => new
                {
                    FlightSearchResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stopovers = table.Column<int>(type: "int", nullable: false),
                    Passengers = table.Column<int>(type: "int", nullable: false),
                    AdultsPassengers = table.Column<int>(type: "int", nullable: false),
                    KidsPassengers = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightSearchRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSearchResults", x => x.FlightSearchResultId);
                    table.ForeignKey(
                        name: "FK_FlightSearchResults_FlightSearchRequests_FlightSearchRequestId",
                        column: x => x.FlightSearchRequestId,
                        principalTable: "FlightSearchRequests",
                        principalColumn: "FlightSearchRequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightSearchResults_FlightSearchRequestId",
                table: "FlightSearchResults",
                column: "FlightSearchRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightSearchResults");

            migrationBuilder.DropTable(
                name: "FlightSearchRequests");
        }
    }
}
