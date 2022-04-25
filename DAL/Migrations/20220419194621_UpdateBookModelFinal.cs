using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateBookModelFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookFile",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "BookAudioFile",
                table: "Book",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookWebFile",
                table: "Book",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookAudioFile",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "BookWebFile",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "BookFile",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
