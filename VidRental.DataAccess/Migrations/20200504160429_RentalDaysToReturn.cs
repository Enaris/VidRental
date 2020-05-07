using Microsoft.EntityFrameworkCore.Migrations;

namespace VidRental.DataAccess.Migrations
{
    public partial class RentalDaysToReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaysToReturn",
                table: "Rental",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysToReturn",
                table: "Rental");
        }
    }
}
