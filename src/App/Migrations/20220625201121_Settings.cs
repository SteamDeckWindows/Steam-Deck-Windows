using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamDeckWindows.Migrations
{
    public partial class Settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tool");

            migrationBuilder.CreateTable(
                name: "EmulatorSetting",
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
                    table.PrimaryKey("PK_EmulatorSetting", x => x.EmulatorSettingId);
                    table.ForeignKey(
                        name: "FK_EmulatorSetting_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "SettingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToolSetting",
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
                    table.PrimaryKey("PK_ToolSetting", x => x.ToolSettingId);
                    table.ForeignKey(
                        name: "FK_ToolSetting_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "SettingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmulatorSetting_SettingId",
                table: "EmulatorSetting",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolSetting_SettingId",
                table: "ToolSetting",
                column: "SettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmulatorSetting");

            migrationBuilder.DropTable(
                name: "ToolSetting");

            migrationBuilder.CreateTable(
                name: "Tool",
                columns: table => new
                {
                    ToolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SettingId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tool", x => x.ToolId);
                    table.ForeignKey(
                        name: "FK_Tool_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "SettingId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tool_SettingId",
                table: "Tool",
                column: "SettingId");
        }
    }
}
