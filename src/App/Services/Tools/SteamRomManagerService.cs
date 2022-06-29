using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;

namespace SteamDeckWindows.Services.Tools
{
    public class SteamRomManagerService : IToolsService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath)
        {
            var client = new GithubClient("SteamGridDB", "steam-rom-manager");
            var latestReleases = await client.GetLatestRelease();
            var latestRelease = latestReleases.assets.Where(x => x.name.StartsWith("Steam-ROM-Manager-portable-") && x.name.EndsWith(".exe")).First();

            subProgressLabel.Content = $"Downloading {latestRelease.name}";
            await client.DownloadFile(latestRelease, subProgressBar, $"{installPath}\\Temp\\");
            var filenameWithoutExt = Path.GetFileNameWithoutExtension($"{installPath}\\Temp\\{latestRelease.name}");
            subProgressLabel.Content = $"Unpacking {latestRelease.name} to {installPath}\\Temp\\{filenameWithoutExt}";
           
            ZipFile.ExtractToDirectory($"{installPath}\\Temp\\{latestRelease.name}", $"{installPath}\\Temp\\{filenameWithoutExt}", true);
            //move
            DirectoryExtensions.MoveDirectory($"{installPath}\\Temp\\{filenameWithoutExt}\\publish", $"{installPath}\\Tools\\SteamRomManager");
            
            //cleanup
            File.Delete($"{installPath}\\Temp\\{latestRelease.name}");

            subProgressLabel.Content = "Finished installing SteamRomManager";
        }
    }
}
