using SteamDeckWindows.Data;
using SteamDeckWindows.Models;
using SteamDeckWindows.Services.Emulators;
using SteamDeckWindows.Services.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Services
{
    public class InstallService
    {
        private ProgressBar progressBar { get; set; }
        private Label progressLabel { get; set; }
        private ProgressBar subProgressBar { get; set; }
        private Label subProgressLabel { get; set; }
        private Setting? setting { get; set; }
        private TextBox status { get; set; }

        public InstallService(TextBox status, ProgressBar progressBar, Label progressLabel, ProgressBar subProgressBar, Label subProgressLabel, Setting? setting)
        {
            this.progressBar = progressBar;
            this.progressLabel = progressLabel;
            this.subProgressBar = subProgressBar;
            this.subProgressLabel = subProgressLabel;
            this.setting = setting;
            this.status = status;
        }

        public async Task Update()
        {
            try {
                AddStatus("Running Update");
                if (setting != null)
                {
                    var steps = GetSteps();
                    AddStatus("Creating directories");
                    progressLabel.Content = $"Step 1 of {steps.Total} - Creating directories";
                    CreateDirectories();
                    GetProgress(1, steps);

                    if (setting.InstallDrivers)
                    {
                        progressLabel.Content = $"Step {steps.InstallDrivers} of {steps.Total} - Installing drivers";
                        await new DriverService().DownloadDrivers(subProgressBar, subProgressLabel, $"{setting.InstallPath}");
                        GetProgress(steps.InstallDrivers, steps);
                    }
                    if (setting.InstallEmulationStationDe)
                    {
                        progressLabel.Content = $"Step {steps.InstallEmulationStationDe} of {steps.Total} - Installing Emulation Station DE";
                        await new EmulationStationDeService().InstallLatest(subProgressBar, subProgressLabel, $"{setting.InstallPath}");
                        GetProgress(steps.InstallEmulationStationDe, steps);
                    }
                    if (setting.Emulators.Where(x => x.Install == true).Any())
                    {
                        progressLabel.Content = $"Step {steps.InstallEmulators} of {steps.Total} - Installing emulators";
                        await InstallEmulators();
                        GetProgress(steps.InstallEmulators, steps);
                    }
                    if (setting.Tools.Where(x => x.Install == true).Any())
                    {
                        progressLabel.Content = $"Step {steps.InstallTools} of {steps.Total} - Installing tools";
                        await InstallTools();
                        GetProgress(steps.InstallTools, steps);
                    }
                    if (setting.Emulators.Where(x => x.Install == true).Any())
                    {
                        progressLabel.Content = $"Step {steps.ConfigureEmulators} of {steps.Total} - Configuring emulators";
                        //TODO configure selected emulators
                        GetProgress(steps.ConfigureEmulators, steps);
                    }
                    if (setting.Tools.Where(x => x.Install == true).Any())
                    {
                        progressLabel.Content = $"Step {steps.ConfigureTools} of {steps.Total} - Configuring tools";
                        //TODO configure selected tools
                        GetProgress(steps.ConfigureTools, steps);
                    }
                    if (setting.InstallEmulationStationDe)
                    {
                        progressLabel.Content = $"Step {steps.ConfigureEmulationStationDe} of {steps.Total} - Configuring Emulation Station DE";
                        //await new EmulationStationDeService().InstallLatest(subProgressBar, subProgressLabel, $"{setting.InstallPath}");
                        GetProgress(steps.ConfigureEmulationStationDe, steps);
                    }
                    AddStatus("Update Complete!");
                    AddStatus(@"\,,/(^‿^)\,,/");
                }
                else
                {
                    AddStatus("An ERROR occured! We could not find valid settings. Please click 'Settings' and review/save your settings. Then run update again.");
                }
            }
            catch(Exception ex){
                AddStatus("Error!");
                AddStatus("┌∩┐(◣_◢)┌∩┐");
                AddStatus("Error Message");
                AddStatus(ex.Message);
                if (ex.StackTrace != null)
                {
                    AddStatus("Error Stacktrace");
                    AddStatus(ex.StackTrace);
                }
                AddStatus("Please report this as an issue if the problem persists.");
            }
        }
        private void AddStatus(string text)
        {
            status.Text += $"{text}\r\n";
            status.ScrollToEnd();
        }

        private Steps GetSteps()
        {
            var steps = new Steps();
            steps.Total = 1;
            if (setting == null) return steps;
            if (setting.InstallDrivers) { steps.Total++; steps.InstallDrivers = steps.Total; }
            if (setting.InstallEmulationStationDe) { steps.Total++; steps.InstallEmulationStationDe = steps.Total; }
            if (setting.Emulators.Where(x=>x.Install == true).Any()) { steps.Total++; steps.InstallEmulators = steps.Total; }
            if (setting.Tools.Where(x => x.Install == true).Any()) { steps.Total++; steps.InstallTools = steps.Total; }
            if (setting.Emulators.Where(x => x.Install == true).Any()) { steps.Total++; steps.ConfigureEmulators = steps.Total; }
            if (setting.Tools.Where(x => x.Install == true).Any()) { steps.Total++; steps.ConfigureTools = steps.Total; }
            if (setting.InstallEmulationStationDe) { steps.Total++; steps.ConfigureEmulationStationDe = steps.Total; }
            return steps;
        }

        private void GetProgress(int current, Steps steps)
        {
            progressBar.Value = (int)(((double)current / (double)steps.Total) * 100);
        }

        private void CreateDirectories()
        {
            if (setting == null) return;
            Directory.CreateDirectory(setting.InstallPath);
            Directory.CreateDirectory($"{setting.InstallPath}\\Temp");
            Directory.CreateDirectory($"{setting.InstallPath}\\Drivers");
            Directory.CreateDirectory($"{setting.InstallPath}\\Emulators");
            Directory.CreateDirectory($"{setting.InstallPath}\\Roms");
            Directory.CreateDirectory($"{setting.InstallPath}\\Tools");
        }

        private async Task InstallEmulators()
        {
            if (setting == null) return;
            foreach (var emulator in setting.Emulators.Where(x => x.Install == true).ToList())
            {
                AddStatus($"Installing {emulator.Name}");
                switch (emulator.Name)
                {
                    case "PCSX2":
                        await new Pcsx2().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    case "PrimeHack":
                        await new PrimeHack().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    case "RetroArch":
                        await new RetroArch().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    case "Ryujinx":
                        await new Ryujinx().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    case "Yuzu":
                        await new Yuzu().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    case "Xemu":
                        await new Xemu().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    default:
                        break;
                }
                AddStatus($"Installed {emulator.Name}");
            }
        }
        private async Task InstallTools()
        {
            if (setting == null) return;
            foreach (var tool in setting.Tools.Where(x => x.Install == true).ToList())
            {
                AddStatus($"Installing {tool.Name}");
                switch (tool.Name)
                {
                    case "EmuSAK":
                        await new EmuSakService().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    case "GlosSI":
                        await new GlosSIService().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    case "Steam Rom Manager":
                        await new SteamRomManagerService().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    default:
                        break;
                }
                AddStatus($"Installed {tool.Name}");
            }
        }
    }

    public class Steps
    {
        public int Total { get; set; }
        public int InstallDrivers { get; set; }
        public int InstallEmulationStationDe { get; set; }
        public int InstallEmulators { get; set; }
        public int InstallTools { get; set; }
        public int ConfigureEmulators { get; set; }
        public int ConfigureTools { get; set; }
        public int ConfigureEmulationStationDe { get; set; }
    }
}
