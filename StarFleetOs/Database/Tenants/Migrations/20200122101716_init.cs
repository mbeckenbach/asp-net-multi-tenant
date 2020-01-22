using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarFleetOs.Database.Tenants.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tnt");

            migrationBuilder.CreateTable(
                name: "AppTenants",
                schema: "tnt",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Identifier = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Domain = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTenants", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTenants",
                schema: "tnt");
        }
    }
}
