using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateBookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookFile",
                table: "Book",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookImage",
                table: "Book",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookFile",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "BookImage",
                table: "Book");
        }
    }
}
