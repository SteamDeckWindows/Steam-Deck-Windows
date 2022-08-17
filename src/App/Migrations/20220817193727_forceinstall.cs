using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamDeckWindows.Migrations
{
    public partial class forceinstall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForceReInstall",
                table: "ToolSettings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RecommendedVersion",
                table: "ToolSettings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ForceReInstall",
                table: "EmulatorSettings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RecommendedVersion",
                table: "EmulatorSettings",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForceReInstall",
                table: "ToolSettings");

            migrationBuilder.DropColumn(
                name: "RecommendedVersion",
                table: "ToolSettings");

            migrationBuilder.DropColumn(
                name: "ForceReInstall",
                table: "EmulatorSettings");

            migrationBuilder.DropColumn(
                name: "RecommendedVersion",
                table: "EmulatorSettings");
        }
    }
}
