using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.DataAccess.Migrations
{
    public partial class AddedDeveloperPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeveloperPhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperPhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeveloperPhoneNumber_Developer_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeveloperPhoneNumbers",
                columns: new[] { "Id", "DeveloperId", "Number" },
                values: new object[] { new Guid("f48fbfb5-1e58-4937-85dd-f7a8e853f605"), new Guid("b0287537-dfe1-4f44-8666-5da587651bf7"), "+51 123-45-67" });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperPhoneNumbers_DeveloperId",
                table: "DeveloperPhoneNumbers",
                column: "DeveloperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperPhoneNumbers");
        }
    }
}
