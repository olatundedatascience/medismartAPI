using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedismartsAPI.Migrations
{
    public partial class errorLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(nullable: true),
                    ControllerName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    RequestBody = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    LineNumber = table.Column<string>(nullable: true),
                    CodeLine = table.Column<string>(nullable: true),
                    DateOccur = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLog");
        }
    }
}
