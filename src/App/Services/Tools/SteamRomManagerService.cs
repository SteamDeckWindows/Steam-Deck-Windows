using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;
using SevenZipExtractor;
using SteamDeckWindows.Models;

namespace SteamDeckWindows.Services.Tools
{
    public class SteamRomManagerService : IToolsService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath, ToolSetting toolSetting, TextBox status)
        {
            var client = new GithubClient("SteamGridDB", "steam-rom-manager");
            var latestReleases = await client.GetLatestRelease();
            var latestRelease = latestReleases.assets.Where(x => x.name.StartsWith("Steam-ROM-Manager-portable-") && x.name.EndsWith(".exe")).First();

            //if(latestRelease.version == toolSetting.InstalledVersion && !toolSetting.ForceReInstall){
            //    status.Text += $"Steam Rom Manager version {latestRelease.version} allready installed skipping download";
            //}

            subProgressLabel.Content = $"Downloading {latestRelease.name}";
            await client.DownloadFile(latestRelease, subProgressBar, $"{installPath}\\Temp\\");

            Directory.CreateDirectory($"{installPath}\\Tools\\SteamRomManager");
            if (File.Exists($"{installPath}\\Tools\\SteamRomManager\\SteamRomManager.exe")) 
                File.Delete($"{installPath}\\Tools\\SteamRomManager\\SteamRomManager.exe");

            File.Move($"{installPath}\\Temp\\{latestRelease.name}", $"{installPath}\\Tools\\SteamRomManager\\SteamRomManager.exe");

            subProgressLabel.Content = "Finished installing Steam Rom Manager";
        }
    }
}
