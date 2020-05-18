using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VidRental.DataAccess.Migrations
{
    public partial class rentalTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_ShopUsers_UserId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_UserId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rental");

            migrationBuilder.AddColumn<Guid>(
                name: "ShopUserId",
                table: "Rental",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rental_ShopUserId",
                table: "Rental",
                column: "ShopUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_ShopUsers_ShopUserId",
                table: "Rental",
                column: "ShopUserId",
                principalTable: "ShopUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_ShopUsers_ShopUserId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_ShopUserId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "ShopUserId",
                table: "Rental");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Rental",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rental_UserId",
                table: "Rental",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_ShopUsers_UserId",
                table: "Rental",
                column: "UserId",
                principalTable: "ShopUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
