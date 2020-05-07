using Microsoft.EntityFrameworkCore.Migrations;

namespace VidRental.DataAccess.Migrations
{
    public partial class RentalExtened : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Delivery",
                table: "Rental",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "RentPrice",
                table: "Rental",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ReturnedOnTime",
                table: "Rental",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivery",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "RentPrice",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "ReturnedOnTime",
                table: "Rental");
        }
    }
}
