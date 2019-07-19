using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Libraary.Data.Migrations
{
    public partial class CategoriesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Id", "CategoryName" },
            values: new object[] { Guid.NewGuid().ToString(), "Art, Music, Photography" });

            migrationBuilder.InsertData(
          table: "Categories",
          columns: new[] { "Id", "CategoryName" },
          values: new object[] { Guid.NewGuid().ToString(), "Business" });

            migrationBuilder.InsertData(
         table: "Categories",
         columns: new[] { "Id", "CategoryName" },
         values: new object[] { Guid.NewGuid().ToString(), "Kids" });

            migrationBuilder.InsertData(
         table: "Categories",
         columns: new[] { "Id", "CategoryName" },
         values: new object[] { Guid.NewGuid().ToString(), "Comics" });

            migrationBuilder.InsertData(
         table: "Categories",
         columns: new[] { "Id", "CategoryName" },
         values: new object[] { Guid.NewGuid().ToString(), "Computers, Tech" });

            migrationBuilder.InsertData(
         table: "Categories",
         columns: new[] { "Id", "CategoryName" },
         values: new object[] { Guid.NewGuid().ToString(), "Cooking" });

            migrationBuilder.InsertData(
         table: "Categories",
         columns: new[] { "Id", "CategoryName" },
         values: new object[] { Guid.NewGuid().ToString(), "Hobbies, Crafts" });

            migrationBuilder.InsertData(
         table: "Categories",
         columns: new[] { "Id", "CategoryName" },
         values: new object[] { Guid.NewGuid().ToString(), "History" });

            migrationBuilder.InsertData(
       table: "Categories",
       columns: new[] { "Id", "CategoryName" },
       values: new object[] { Guid.NewGuid().ToString(), "Health, Fitness" });

            migrationBuilder.InsertData(
       table: "Categories",
       columns: new[] { "Id", "CategoryName" },
       values: new object[] { Guid.NewGuid().ToString(), "Home, Garden" });

            migrationBuilder.InsertData(
       table: "Categories",
       columns: new[] { "Id", "CategoryName" },
       values: new object[] { Guid.NewGuid().ToString(), "Horror" });

            migrationBuilder.InsertData(
       table: "Categories",
       columns: new[] { "Id", "CategoryName" },
       values: new object[] { Guid.NewGuid().ToString(), "Entertainment" });

            migrationBuilder.InsertData(
       table: "Categories",
       columns: new[] { "Id", "CategoryName" },
       values: new object[] { Guid.NewGuid().ToString(), "Medical" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
