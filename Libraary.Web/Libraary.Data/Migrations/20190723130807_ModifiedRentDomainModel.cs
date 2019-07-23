using Microsoft.EntityFrameworkCore.Migrations;

namespace Libraary.Data.Migrations
{
    public partial class ModifiedRentDomainModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rents",
                table: "Rents");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Rents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rents",
                table: "Rents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_BookId",
                table: "Rents",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rents",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_BookId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Rents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rents",
                table: "Rents",
                columns: new[] { "BookId", "UserId" });
        }
    }
}
