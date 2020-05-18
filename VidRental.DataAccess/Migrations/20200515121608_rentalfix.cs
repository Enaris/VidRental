using Microsoft.EntityFrameworkCore.Migrations;

namespace VidRental.DataAccess.Migrations
{
    public partial class rentalfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_CartridgeCopies_CartridgeCopyId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_ShopUsers_ShopUserId",
                table: "Rental");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_CartridgeCopies_CartridgeCopyId",
                table: "Rental",
                column: "CartridgeCopyId",
                principalTable: "CartridgeCopies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_ShopUsers_ShopUserId",
                table: "Rental",
                column: "ShopUserId",
                principalTable: "ShopUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_CartridgeCopies_CartridgeCopyId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_ShopUsers_ShopUserId",
                table: "Rental");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_CartridgeCopies_CartridgeCopyId",
                table: "Rental",
                column: "CartridgeCopyId",
                principalTable: "CartridgeCopies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_ShopUsers_ShopUserId",
                table: "Rental",
                column: "ShopUserId",
                principalTable: "ShopUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
