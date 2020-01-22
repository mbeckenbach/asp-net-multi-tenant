using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarFleetOs.Database.App.Migrations
{
    public partial class tenantEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                schema: "app",
                table: "CrewMembers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "app",
                table: "CrewMembers");
        }
    }
}
