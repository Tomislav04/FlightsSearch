using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class SomeChangesOnModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultsPassengers",
                table: "FlightSearchResults");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "FlightSearchResults");

            migrationBuilder.DropColumn(
                name: "Passengers",
                table: "FlightSearchRequests");

            migrationBuilder.RenameColumn(
                name: "Stopovers",
                table: "FlightSearchResults",
                newName: "StopoversReturn");

            migrationBuilder.RenameColumn(
                name: "Passengers",
                table: "FlightSearchResults",
                newName: "StopoversDeparture");

            migrationBuilder.RenameColumn(
                name: "KidsPassengers",
                table: "FlightSearchResults",
                newName: "PassengersNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StopoversReturn",
                table: "FlightSearchResults",
                newName: "Stopovers");

            migrationBuilder.RenameColumn(
                name: "StopoversDeparture",
                table: "FlightSearchResults",
                newName: "Passengers");

            migrationBuilder.RenameColumn(
                name: "PassengersNumber",
                table: "FlightSearchResults",
                newName: "KidsPassengers");

            migrationBuilder.AddColumn<int>(
                name: "AdultsPassengers",
                table: "FlightSearchResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "FlightSearchResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Passengers",
                table: "FlightSearchRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
