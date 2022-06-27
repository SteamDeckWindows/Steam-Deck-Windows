using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamDeckWindows.Migrations
{
    public partial class InstallPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstallPath",
                table: "Settings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallPath",
                table: "Settings");
        }
    }
}
