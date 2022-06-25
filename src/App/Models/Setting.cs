using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SteamDeckWindows.Models
{
    public class Setting
    {
        public int SettingId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ToolSetting> Tools { get; private set; } = new ObservableCollection<ToolSetting>();
        public virtual ICollection<EmulatorSetting> Emulators { get; private set; } = new ObservableCollection<EmulatorSetting>();
    }
    public class ToolSetting
    {
        public int ToolSettingId { get; set; }
        public string Name { get; set; }
        public bool Install { get; set; }
        public virtual Setting Setting { get; private set; }
}

    public class EmulatorSetting
    {
        public int EmulatorSettingId { get; set; }
        public string Name { get; set; }
        public bool Install { get; set; }
        public bool ResetSettings { get; set; }
        public virtual Setting Setting { get; private set; }
    }
}
