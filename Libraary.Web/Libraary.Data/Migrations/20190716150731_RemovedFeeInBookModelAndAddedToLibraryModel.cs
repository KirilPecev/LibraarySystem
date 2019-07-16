using Microsoft.EntityFrameworkCore.Migrations;

namespace Libraary.Data.Migrations
{
    public partial class RemovedFeeInBookModelAndAddedToLibraryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Books");

            migrationBuilder.AddColumn<decimal>(
                name: "BooksFee",
                table: "Libraries",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BooksFee",
                table: "Libraries");

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "Books",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
