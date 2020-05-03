using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace test2.Migrations
{
    public partial class performances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PerformanceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceDates_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    PerformanceDateId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceTimes_PerformanceDates_PerformanceDateId",
                        column: x => x.PerformanceDateId,
                        principalTable: "PerformanceDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceDates_PerformanceId",
                table: "PerformanceDates",
                column: "PerformanceId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceTimes_PerformanceDateId",
                table: "PerformanceTimes",
                column: "PerformanceDateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceTimes");

            migrationBuilder.DropTable(
                name: "PerformanceDates");

            migrationBuilder.DropTable(
                name: "Performances");
        }
    }
}
