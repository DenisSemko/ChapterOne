using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FinalUpdBookCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookCollection_BookCollectionId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_BookCollection_BookCollectionId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_BookCollectionId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Book_BookCollectionId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "BookCollectionId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "BookCollectionId",
                table: "Book");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "BookCollection",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "BookCollection",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCollection_BookId",
                table: "BookCollection",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCollection_CategoryId",
                table: "BookCollection",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollection_Book_BookId",
                table: "BookCollection",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollection_Category_CategoryId",
                table: "BookCollection",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCollection_Book_BookId",
                table: "BookCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCollection_Category_CategoryId",
                table: "BookCollection");

            migrationBuilder.DropIndex(
                name: "IX_BookCollection_BookId",
                table: "BookCollection");

            migrationBuilder.DropIndex(
                name: "IX_BookCollection_CategoryId",
                table: "BookCollection");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BookCollection");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BookCollection");

            migrationBuilder.AddColumn<Guid>(
                name: "BookCollectionId",
                table: "Category",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookCollectionId",
                table: "Book",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_BookCollectionId",
                table: "Category",
                column: "BookCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookCollectionId",
                table: "Book",
                column: "BookCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookCollection_BookCollectionId",
                table: "Book",
                column: "BookCollectionId",
                principalTable: "BookCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_BookCollection_BookCollectionId",
                table: "Category",
                column: "BookCollectionId",
                principalTable: "BookCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
