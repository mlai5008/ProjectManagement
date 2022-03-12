using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.DataAccess.Migrations
{
    public partial class SeedingItemsDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("b0287537-dfe1-4f44-8666-5da587651bf7"), null, "Larry", "Page" },
                    { new Guid("7b6bd555-c9f8-42a7-ac8f-698c2ba36646"), null, "Bill", "Gates" },
                    { new Guid("fb1459c7-393f-45b6-a662-dce000d9aac0"), null, "Mark", "Zuckerberg" },
                    { new Guid("08d38390-b579-40bc-8396-e355d52bc823"), null, "Ken", "Thompson" },
                    { new Guid("3aaaa471-54c9-4162-844d-9accf7a6ad80"), null, "Linus", "Torvalds" },
                    { new Guid("1ea74fc5-e844-4f70-b37f-ac2c9d6cb43c"), null, "Ada", "Lovelace" },
                    { new Guid("0bcd05a8-f538-48f1-a1d2-382f5e949c11"), null, "Alan", "Turing" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("08d38390-b579-40bc-8396-e355d52bc823"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0bcd05a8-f538-48f1-a1d2-382f5e949c11"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("1ea74fc5-e844-4f70-b37f-ac2c9d6cb43c"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("3aaaa471-54c9-4162-844d-9accf7a6ad80"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7b6bd555-c9f8-42a7-ac8f-698c2ba36646"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("b0287537-dfe1-4f44-8666-5da587651bf7"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("fb1459c7-393f-45b6-a662-dce000d9aac0"));
        }
    }
}
