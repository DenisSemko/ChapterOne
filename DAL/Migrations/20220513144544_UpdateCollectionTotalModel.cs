using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateCollectionTotalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCollection_Category_CategoryId",
                table: "BookCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCollection_AspNetUsers_UserId",
                table: "BookCollection");

            migrationBuilder.DropIndex(
                name: "IX_BookCollection_CategoryId",
                table: "BookCollection");

            migrationBuilder.DropIndex(
                name: "IX_BookCollection_UserId",
                table: "BookCollection");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BookCollection");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BookCollection");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookCollection");

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionId",
                table: "BookCollection",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collection_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collection_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCollection_CollectionId",
                table: "BookCollection",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_CategoryId",
                table: "Collection",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_Name",
                table: "Collection",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_UserId",
                table: "Collection",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollection_Collection_CollectionId",
                table: "BookCollection",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCollection_Collection_CollectionId",
                table: "BookCollection");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropIndex(
                name: "IX_BookCollection_CollectionId",
                table: "BookCollection");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "BookCollection");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "BookCollection",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BookCollection",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "BookCollection",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCollection_CategoryId",
                table: "BookCollection",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCollection_UserId",
                table: "BookCollection",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollection_Category_CategoryId",
                table: "BookCollection",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollection_AspNetUsers_UserId",
                table: "BookCollection",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
