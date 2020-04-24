using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VidRental.DataAccess.Migrations
{
    public partial class movieCartridgeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    RealeaseDate = table.Column<DateTime>(nullable: false),
                    Director = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cartridges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Language = table.Column<string>(nullable: false),
                    Avaible = table.Column<bool>(nullable: false),
                    RentPrice = table.Column<decimal>(type: "money", nullable: false),
                    DaysToReturn = table.Column<int>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true),
                    MovieId = table.Column<string>(nullable: false),
                    MovieId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartridges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartridges_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cartridges_Movies_MovieId1",
                        column: x => x.MovieId1,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageType = table.Column<string>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieImages_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieImages_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartridges_ImageId",
                table: "Cartridges",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartridges_MovieId1",
                table: "Cartridges",
                column: "MovieId1");

            migrationBuilder.CreateIndex(
                name: "IX_MovieImages_ImageId",
                table: "MovieImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieImages_MovieId",
                table: "MovieImages",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartridges");

            migrationBuilder.DropTable(
                name: "MovieImages");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
