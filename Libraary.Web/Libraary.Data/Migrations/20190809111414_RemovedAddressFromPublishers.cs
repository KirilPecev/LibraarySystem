using Microsoft.EntityFrameworkCore.Migrations;

namespace Libraary.Data.Migrations
{
    public partial class RemovedAddressFromPublishers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Addresses_AddressId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_AddressId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Publishers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressId",
                table: "Publishers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Publishers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_AddressId",
                table: "Publishers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Addresses_AddressId",
                table: "Publishers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
