using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamDeckWindows.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    InstallDrivers = table.Column<bool>(type: "INTEGER", nullable: false),
                    InstallEmulationStationDe = table.Column<bool>(type: "INTEGER", nullable: false),
                    ResetEmulationStationDe = table.Column<bool>(type: "INTEGER", nullable: false),
                    RetroAchievementsUsername = table.Column<string>(type: "TEXT", nullable: true),
                    RetroAchievementsPassword = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingId);
                });

            migrationBuilder.CreateTable(
                name: "EmulatorSettings",
                columns: table => new
                {
                    EmulatorSettingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Install = table.Column<bool>(type: "INTEGER", nullable: false),
                    ResetSettings = table.Column<bool>(type: "INTEGER", nullable: false),
                    SettingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmulatorSettings", x => x.EmulatorSettingId);
                    table.ForeignKey(
                        name: "FK_EmulatorSettings_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "SettingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToolSettings",
                columns: table => new
                {
                    ToolSettingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Install = table.Column<bool>(type: "INTEGER", nullable: false),
                    SettingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolSettings", x => x.ToolSettingId);
                    table.ForeignKey(
                        name: "FK_ToolSettings_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "SettingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmulatorSettings_SettingId",
                table: "EmulatorSettings",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolSettings_SettingId",
                table: "ToolSettings",
                column: "SettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmulatorSettings");

            migrationBuilder.DropTable(
                name: "ToolSettings");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
