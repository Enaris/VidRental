using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VidRental.DataAccess.Migrations
{
    public partial class CartridgeMovieIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartridges_Movies_MovieId1",
                table: "Cartridges");

            migrationBuilder.DropIndex(
                name: "IX_Cartridges_MovieId1",
                table: "Cartridges");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "Cartridges");

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieId",
                table: "Cartridges",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Cartridges_MovieId",
                table: "Cartridges",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartridges_Movies_MovieId",
                table: "Cartridges",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartridges_Movies_MovieId",
                table: "Cartridges");

            migrationBuilder.DropIndex(
                name: "IX_Cartridges_MovieId",
                table: "Cartridges");

            migrationBuilder.AlterColumn<string>(
                name: "MovieId",
                table: "Cartridges",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId1",
                table: "Cartridges",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cartridges_MovieId1",
                table: "Cartridges",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartridges_Movies_MovieId1",
                table: "Cartridges",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
