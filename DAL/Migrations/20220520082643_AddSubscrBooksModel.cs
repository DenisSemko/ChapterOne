using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddSubscrBooksModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionsBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubscriptionId = table.Column<Guid>(nullable: true),
                    BookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionsBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionsBooks_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionsBooks_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionsBooks_BookId",
                table: "SubscriptionsBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionsBooks_SubscriptionId",
                table: "SubscriptionsBooks",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionsBooks");
        }
    }
}
