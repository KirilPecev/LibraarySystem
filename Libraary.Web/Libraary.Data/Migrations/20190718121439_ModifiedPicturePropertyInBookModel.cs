using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Libraary.Data.Migrations
{
    public partial class ModifiedPicturePropertyInBookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "Books",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "Books");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Books",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
