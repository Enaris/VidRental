using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VidRental.DataAccess.Migrations
{
    public partial class addedCartridgeCopies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartridges_Image_ImageId",
                table: "Cartridges");

            migrationBuilder.DropIndex(
                name: "IX_Cartridges_ImageId",
                table: "Cartridges");

            migrationBuilder.DropColumn(
                name: "Avaible",
                table: "Cartridges");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Cartridges");

            migrationBuilder.CreateTable(
                name: "CartridgeCopies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CartridgeId = table.Column<Guid>(nullable: false),
                    Avaible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartridgeCopies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartridgeCopies_Cartridges_CartridgeId",
                        column: x => x.CartridgeId,
                        principalTable: "Cartridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartridgeCopies_CartridgeId",
                table: "CartridgeCopies",
                column: "CartridgeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartridgeCopies");

            migrationBuilder.AddColumn<bool>(
                name: "Avaible",
                table: "Cartridges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Cartridges",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cartridges_ImageId",
                table: "Cartridges",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartridges_Image_ImageId",
                table: "Cartridges",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
