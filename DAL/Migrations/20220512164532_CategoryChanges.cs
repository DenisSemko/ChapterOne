using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CategoryChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCollection_Category_CategoryId",
                table: "BookCollection");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookCollection",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCollection_Name",
                table: "BookCollection",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollection_Category_CategoryId",
                table: "BookCollection",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCollection_Category_CategoryId",
                table: "BookCollection");

            migrationBuilder.DropIndex(
                name: "IX_BookCollection_Name",
                table: "BookCollection");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookCollection",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCollection_Category_CategoryId",
                table: "BookCollection",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
