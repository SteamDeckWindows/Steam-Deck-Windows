using SteamDeckWindows.Data;
using SteamDeckWindows.Models;
using SteamDeckWindows.Services.Emulators;
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
                    //await new DriverService().DownloadDrivers(subProgressBar, subProgressLabel, $"{setting.InstallPath}");
                    GetProgress(steps.InstallDrivers, steps);
                }
                if (setting.InstallEmulationStationDe)
                {
                    progressLabel.Content = $"Step {steps.InstallEmulationStationDe} of {steps.Total} - Installing drivers";
                    //await new EmulationStationDeService().InstallLatest(subProgressBar, subProgressLabel, $"{setting.InstallPath}");
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
                    //TODO install selected tools
                    GetProgress(steps.InstallTools, steps);
                }
                if (setting.Emulators.Where(x => x.Install == true).Any())
                {
                    progressLabel.Content = $"Step {steps.ConfigureEmulators} of {steps.Total} - Installing emulators";
                    //TODO configure selected emulators
                    GetProgress(steps.ConfigureEmulators, steps);
                }
                if (setting.Tools.Where(x => x.Install == true).Any())
                {
                    progressLabel.Content = $"Step {steps.ConfigureTools} of {steps.Total} - Installing tools";
                    //TODO configure selected tools
                    GetProgress(steps.ConfigureTools, steps);
                }
            }
            else
            {
                AddStatus("An ERROR occured! We could not find valid settings. Please click 'Settings' and review/save your settings. Then run update again.");
            }
        }
        private void AddStatus(string text)
        {
            status.Text += $"{text}\r\n";
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
                switch (emulator.Name)
                {
                    case "Ryujinx":
                        await new Ryujinx().Install(subProgressBar, subProgressLabel, setting.InstallPath);
                        break;
                    default:
                        break;
                }
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
    }
}
