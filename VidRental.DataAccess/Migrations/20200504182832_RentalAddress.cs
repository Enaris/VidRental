using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VidRental.DataAccess.Migrations
{
    public partial class RentalAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Rental",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rental_AddressId",
                table: "Rental",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Addresses_AddressId",
                table: "Rental",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Addresses_AddressId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_AddressId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Rental");
        }
    }
}
