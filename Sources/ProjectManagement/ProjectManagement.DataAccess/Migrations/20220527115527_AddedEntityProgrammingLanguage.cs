using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.DataAccess.Migrations
{
    public partial class AddedEntityProgrammingLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProgrammingLanguageId",
                table: "Developers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProgrammingLanguages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("e075df46-8637-4395-866a-8e322ba6663c"), "Python" },
                    { new Guid("2fcf3a18-54dd-4bb2-99a7-edc79052585a"), "Java" },
                    { new Guid("0065a6c7-4140-4cc1-b0ec-71ba82894fe3"), "Ruby" },
                    { new Guid("749856eb-7f8a-47e5-bd1c-f6ab09502d99"), "C#" },
                    { new Guid("96118808-4bca-4299-9195-f655c019e5ac"), "C++" },
                    { new Guid("18fd93eb-1cc6-4297-8cf5-92730e25cd81"), "JavaScript" },
                    { new Guid("8697765d-ff50-415c-866f-696b23f6cea5"), "Go" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Developers_ProgrammingLanguageId",
                table: "Developers",
                column: "ProgrammingLanguageId",
                unique: true,
                filter: "[ProgrammingLanguageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Developer_ProgrammingLanguage_ProgrammingLanguageId",
                table: "Developers",
                column: "ProgrammingLanguageId",
                principalTable: "ProgrammingLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developer_ProgrammingLanguage_ProgrammingLanguageId",
                table: "Developers");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Developers_ProgrammingLanguageId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageId",
                table: "Developers");
        }
    }
}
