using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Libraary.Data.Migrations
{
    public partial class AddedLibraryAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Books",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "LibraryId",
                table: "Authors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_LibraryId",
                table: "Authors",
                column: "LibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Libraries_LibraryId",
                table: "Authors",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Libraries_LibraryId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_LibraryId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Authors");
        }
    }
}
