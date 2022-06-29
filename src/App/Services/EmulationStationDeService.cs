using SteamDeckWindows.Clients;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Services
{
    public class EmulationStationDeService
    {
        public async Task InstallLatest(ProgressBar progressBar, Label subProgressLabel, string savePath)
        {
            subProgressLabel.Content = "Installing EmulationStation-DE";
            var client = new GitlabClient("es-de", "emulationstation-de");
            var releases = await client.GetReleases();
            var latestRelease = releases.Where(w=>w.name.Equals("ES-DE_Stable")).OrderByDescending(o => o.created_at).First();
            var files = await client.GetRelease(latestRelease.id.ToString());
            var windowsPortablePackage = files.FirstOrDefault(x => x.file_name.Contains("x64_Portable"));
            if(windowsPortablePackage != null)
            {
                subProgressLabel.Content = $"Downloading EmulationStation-DE {windowsPortablePackage.file_name}";
                await client.DownloadFile(windowsPortablePackage, progressBar, $"{savePath}\\Temp\\");
                subProgressLabel.Content = $"Unpacking EmulationStation-DE {windowsPortablePackage.file_name} to {savePath}\\Temp\\{windowsPortablePackage.file_name}";
                ZipFile.ExtractToDirectory($"{savePath}\\Temp\\{windowsPortablePackage.file_name}", savePath, true);
                File.Delete($"{savePath}\\Temp\\{windowsPortablePackage.file_name}");
                subProgressLabel.Content = "Finished installing EmulationStation-DE";
            }
            else
            {
                subProgressLabel.Content = "ERROR: EmulationStation-DE portable latest build could not be found!";
            }
        }

        //Create shortcut using EmulationStation.exe --resolution 1281 800 as workaround for screen bug
    }
}