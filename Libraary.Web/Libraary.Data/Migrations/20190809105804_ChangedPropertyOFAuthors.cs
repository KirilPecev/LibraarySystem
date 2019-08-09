using Microsoft.EntityFrameworkCore.Migrations;

namespace Libraary.Data.Migrations
{
    public partial class ChangedPropertyOFAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Addresses_AddressId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_AddressId",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Authors",
                newName: "Nationality");

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Authors",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "Authors",
                newName: "AddressId");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "Authors",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AddressId",
                table: "Authors",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Addresses_AddressId",
                table: "Authors",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
