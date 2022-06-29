using SteamDeckWindows.Models;
using System.Collections.Generic;
using System.Linq;

namespace SteamDeckWindows.Data
{
    public class SeedSetting
    {
        public static void SeedSettingsData(DatabaseContext db, bool resetToDefault = false)
        {
            Setting setting = new Setting
            {
                Name = "MySetting",
                InstallPath = $"C:\\SDW\\",
                InstallDrivers = true,
                InstallEmulationStationDe = true,
                ResetEmulationStationDe = true,
                SettingId = 1
            };
            var exist = db.Settings.FirstOrDefault(x => x.SettingId == 1);
            if(exist != null && resetToDefault)
            {
                db.RemoveRange(db.EmulatorSettings);
                db.RemoveRange(db.ToolSettings);
                db.SaveChanges();

                db.Remove(exist);
                db.SaveChanges();
            }
            if (exist == null || resetToDefault)
            {
                db.Settings.Add(setting);
                db.SaveChanges();

                db.AddRange(EmulatorSettings);
                db.AddRange(ToolSettings);
                db.SaveChanges();
            }
            var es = db.EmulatorSettings.ToList();
            var newEs = new List<EmulatorSetting>();
            foreach(var emu in EmulatorSettings)
            {
                if (es.FirstOrDefault(x => x.EmulatorSettingId.Equals(emu.EmulatorSettingId)) == null)
                    newEs.Add(emu);
            }
            if (newEs.Count() != 0)
            {
                db.AddRange(newEs);
                db.SaveChanges();
            }
            var ts = db.ToolSettings.ToList();
            var newTs = new List<ToolSetting>();
            foreach (var tool in ToolSettings)
            {
                if (ts.FirstOrDefault(x => x.ToolSettingId.Equals(tool.ToolSettingId)) == null)
                    newTs.Add(tool);
            }
            if (newTs.Count() != 0)
            {
                db.AddRange(newTs);
                db.SaveChanges();
            }
        }

        //ADD new emulators this list and also create the requires Service/Emulators/xxService
        public static readonly List<EmulatorSetting> EmulatorSettings = new List<EmulatorSetting>(){
            new EmulatorSetting{ Name = "Ryujinx", SettingId = 1, EmulatorSettingId = 1, Install = true, ResetSettings = true },
            new EmulatorSetting{ Name = "Yuzy", SettingId = 1, EmulatorSettingId = 2, Install = true, ResetSettings = true },
            new EmulatorSetting{ Name = "RetroArch", SettingId = 1, EmulatorSettingId = 3, Install = true, ResetSettings = true },
            new EmulatorSetting{ Name = "Xemu", SettingId = 1, EmulatorSettingId = 4, Install = true, ResetSettings = true },
            // TODO: Want to add these Emulators as Standalone below
            // Cemu does not have api accessible version stuff - direct download for now
            // Dolphin does not have api accessible version stuff - direct download for now
            // RPCS3
            // Citra
            // PPSSPP
            // Duckstation
            // ----- Below this line are emulators where I am not sure if we should just use RetroArch
            // Mupen64Plus-Next
            // OpenBOR
            // Daphne
            // Redream
            // ScummVM
            // DosBox
            // Ports
        };
        //ADD new tools this list and also create the requires Service/Tools/xxService
        public static readonly List<ToolSetting> ToolSettings = new List<ToolSetting>()
        {
            new ToolSetting{ Name = "GlosSI", SettingId = 1, ToolSettingId = 1, Install = true},
            new ToolSetting{ Name = "Steam Rom Manager", SettingId = 1, ToolSettingId = 2, Install = true}
        };
    }
}
