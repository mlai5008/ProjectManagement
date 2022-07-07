using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.DataAccess.Migrations
{
    public partial class AddedMeeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperMeeting",
                columns: table => new
                {
                    DevelopersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperMeeting", x => new { x.DevelopersId, x.MeetingsId });
                    table.ForeignKey(
                        name: "FK_DeveloperMeeting_Developers_DevelopersId",
                        column: x => x.DevelopersId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeveloperMeeting_Meetings_MeetingsId",
                        column: x => x.MeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Meetings",
                columns: new[] { "Id", "DateFrom", "DateTo", "Title" },
                values: new object[] { new Guid("2ef776fe-0b95-4f13-b299-b49b81eb33db"), new DateTime(2022, 7, 5, 9, 35, 30, 318, DateTimeKind.Local).AddTicks(7974), new DateTime(2022, 7, 4, 9, 35, 30, 316, DateTimeKind.Local).AddTicks(8590), "Watching Soccer" });

            migrationBuilder.InsertData(
                table: "DeveloperMeeting",
                columns: new[] { "DevelopersId", "MeetingsId" },
                values: new object[] { new Guid("b0287537-dfe1-4f44-8666-5da587651bf7"), new Guid("2ef776fe-0b95-4f13-b299-b49b81eb33db") });

            migrationBuilder.InsertData(
                table: "DeveloperMeeting",
                columns: new[] { "DevelopersId", "MeetingsId" },
                values: new object[] { new Guid("7b6bd555-c9f8-42a7-ac8f-698c2ba36646"), new Guid("2ef776fe-0b95-4f13-b299-b49b81eb33db") });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperMeeting_MeetingsId",
                table: "DeveloperMeeting",
                column: "MeetingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperMeeting");

            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
