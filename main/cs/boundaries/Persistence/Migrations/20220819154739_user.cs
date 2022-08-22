using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wepsys.DianaHr.Persistence.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dianahr");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dianahr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "dianahr");
        }
    }
}
