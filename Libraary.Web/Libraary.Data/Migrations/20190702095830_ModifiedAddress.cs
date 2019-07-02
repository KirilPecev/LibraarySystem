using Microsoft.EntityFrameworkCore.Migrations;

namespace Libraary.Data.Migrations
{
    public partial class ModifiedAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Addresses",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Addresses");
        }
    }
}
