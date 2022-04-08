using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class BooksAndRelated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AudioFile = table.Column<string>(nullable: true),
                    WebFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    PublishedDate = table.Column<DateTime>(nullable: false),
                    GenreId = table.Column<Guid>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    ReadingAge = table.Column<int>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: true),
                    FileId = table.Column<Guid>(nullable: true),
                    IsQualified = table.Column<bool>(nullable: false),
                    UploadDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_BookFile_FileId",
                        column: x => x.FileId,
                        principalTable: "BookFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_BookImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "BookImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Combination",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReaderId = table.Column<Guid>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    GenreId = table.Column<Guid>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    IsSuccessful = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Combination_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Combination_AspNetUsers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_FileId",
                table: "Book",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_ImageId",
                table: "Book",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Combination_GenreId",
                table: "Combination",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Combination_ReaderId",
                table: "Combination",
                column: "ReaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Combination");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "BookFile");

            migrationBuilder.DropTable(
                name: "BookImage");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
