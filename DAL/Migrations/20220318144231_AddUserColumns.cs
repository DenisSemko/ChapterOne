using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddUserColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "TimePaid",
                table: "Subscription");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscriptionPaid",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeSubscriptionPaid",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscriptionPaid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TimeSubscriptionPaid",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Subscription",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePaid",
                table: "Subscription",
                type: "datetime2",
                nullable: true);
        }
    }
}
