using Microsoft.EntityFrameworkCore.Migrations;

namespace Libraary.Data.Migrations
{
    public partial class Modif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryBooks_Libraries_LibraryId",
                table: "LibraryBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryBooks_Libraries_LibraryId",
                table: "LibraryBooks",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryBooks_Libraries_LibraryId",
                table: "LibraryBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryBooks_Libraries_LibraryId",
                table: "LibraryBooks",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
