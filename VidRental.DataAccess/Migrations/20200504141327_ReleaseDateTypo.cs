using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VidRental.DataAccess.Migrations
{
    public partial class ReleaseDateTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealeaseDate",
                table: "Movies");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Rented = table.Column<DateTime>(nullable: false),
                    Returned = table.Column<DateTime>(nullable: true),
                    CartridgeCopyId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rental_CartridgeCopies_CartridgeCopyId",
                        column: x => x.CartridgeCopyId,
                        principalTable: "CartridgeCopies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rental_ShopUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ShopUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rental_CartridgeCopyId",
                table: "Rental",
                column: "CartridgeCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_UserId",
                table: "Rental",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rental");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movies");

            migrationBuilder.AddColumn<DateTime>(
                name: "RealeaseDate",
                table: "Movies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
