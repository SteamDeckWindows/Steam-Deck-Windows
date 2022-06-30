using SteamDeckWindows.Models;
using System.Collections.Generic;
using System.Linq;

namespace SteamDeckWindows.Data
{
    public class SeedSetting
    {
        //ADD new emulators this list and also create the requires Service/Emulators/xxService
        public static readonly List<EmulatorSetting> EmulatorSettings = new List<EmulatorSetting>(){
            new EmulatorSetting{ Name = "Ryujinx", SettingId = 1, EmulatorSettingId = 1, Install = true, ResetSettings = true, ForceReInstall = false },
            new EmulatorSetting{ Name = "Yuzu", SettingId = 1, EmulatorSettingId = 2, Install = true, ResetSettings = true, ForceReInstall = false },
            new EmulatorSetting{ Name = "RetroArch", SettingId = 1, EmulatorSettingId = 3, Install = true, ResetSettings = true, ForceReInstall = false },
            new EmulatorSetting{ Name = "Xemu", SettingId = 1, EmulatorSettingId = 4, Install = true, ResetSettings = true, ForceReInstall = false },
            new EmulatorSetting{ Name = "PCSX2", SettingId = 1, EmulatorSettingId = 5, Install = true, ResetSettings = true, ForceReInstall = false },
            new EmulatorSetting{ Name = "PrimeHack", SettingId = 1, EmulatorSettingId = 6, Install = true, ResetSettings = true, ForceReInstall = false },
            new EmulatorSetting{ Name = "RPCS3", SettingId = 1, EmulatorSettingId = 7, Install = true, ResetSettings = true, ForceReInstall = false },
            // Standalone where specific version update is needed. Update version below
            new EmulatorSetting{ Name = "PPSSPP", SettingId = 1, EmulatorSettingId = 8, Install = true, ResetSettings = true, ForceReInstall = false, RecommendedVersion = "1_12_3" },
            new EmulatorSetting{ Name = "Cemu", SettingId = 1, EmulatorSettingId = 8, Install = true, ResetSettings = true, ForceReInstall = false, RecommendedVersion = "1_12_3" },
            new EmulatorSetting{ Name = "Dolphin", SettingId = 1, EmulatorSettingId = 8, Install = true, ResetSettings = true, ForceReInstall = false, RecommendedVersion = "1_12_3" },
            new EmulatorSetting{ Name = "Citra", SettingId = 1, EmulatorSettingId = 8, Install = true, ResetSettings = true, ForceReInstall = false, RecommendedVersion = "1_12_3" },
            new EmulatorSetting{ Name = "Duckstation", SettingId = 1, EmulatorSettingId = 8, Install = true, ResetSettings = true, ForceReInstall = false, RecommendedVersion = "1_12_3" },
            // ----- Below this line are emulators where I am not sure if we should just use RetroArch
            // MAME - if yes which version(s)
            // Final Burn Alpha - if yes which version(s)
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
            new ToolSetting{ Name = "Steam Rom Manager", SettingId = 1, ToolSettingId = 2, Install = true},
            new ToolSetting{ Name = "EmuSAK", SettingId = 1, ToolSettingId = 3, Install = true}
        };

        // Seed data method
        public static void SeedSettingsData(DatabaseContext db, bool resetToDefault = false)
        {
            Setting setting = new Setting
            {
                Name = "MySetting",
                InstallPath = $"C:\\SDW",
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
            if(exist != null && !resetToDefault){ //We will only update versions and add new emulators and tools
                var es = db.EmulatorSettings.ToList();
                var newEs = new List<EmulatorSetting>();
                foreach(var emu in EmulatorSettings)
                {
                    var existing = es.FirstOrDefault(x => x.EmulatorSettingId.Equals(emu.EmulatorSettingId));
                    if (existing == null){
                        newEs.Add(emu);
                    } else {
                        existing.RecommendedVersion = emu.RecommendedVersion;
                        db.Update(existing);
                        db.SaveChanges();
                    }

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
                    var existing = ts.FirstOrDefault(x => x.ToolSettingId.Equals(tool.ToolSettingId));
                    if (existing == null){
                        newTs.Add(tool);
                    } else {
                        existing.RecommendedVersion = tool.RecommendedVersion;
                        db.Update(existing);
                        db.SaveChanges();
                    }      
                }
                if (newTs.Count() != 0)
                {
                    db.AddRange(newTs);
                    db.SaveChanges();
                }
            }
        }
    }
}
