using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SteamDeckWindows.Models
{
    public class Setting
    {
        public int SettingId { get; set; }
        public string Name { get; set; }
        public string InstallPath { get; set; }
        public bool InstallDrivers { get; set; }
        public bool InstallEmulationStationDe { get; set; }
        public bool ResetEmulationStationDe { get; set; }
        public string? RetroAchievementsUsername { get; set; }
        public string? RetroAchievementsPassword { get; set; }
        public virtual ICollection<ToolSetting> Tools { get; private set; } = new ObservableCollection<ToolSetting>();
        public virtual ICollection<EmulatorSetting> Emulators { get; private set; } = new ObservableCollection<EmulatorSetting>();
    }
    public class ToolSetting
    {
        public int ToolSettingId { get; set; }
        public string Name { get; set; }
        public string? InstalledVersion { get; set; }
        public string? RecommendedVersion { get; set; }
        public bool Install { get; set; }
        public bool ForceReInstall { get; set; }
        internal int SettingId;
        public virtual Setting Setting { get; private set; }
}

    public class EmulatorSetting
    {
        public int EmulatorSettingId { get; set; }
        public string Name { get; set; }
        public string? InstalledVersion { get; set; }
        public string? RecommendedVersion { get; set; }
        public bool Install { get; set; }
        public bool ForceReInstall { get; set; }
        public bool ResetSettings { get; set; }
        internal int SettingId;
        public virtual Setting Setting { get; private set; }
    }
}
