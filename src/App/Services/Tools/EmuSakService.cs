using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;

namespace SteamDeckWindows.Services.Tools
{
    public class EmuSakService : IToolsService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath)
        {
            var client = new GithubClient("CapitaineJSparrow", "emusak-ui");
            var latestReleases = await client.GetLatestRelease();
            var latestRelease = latestReleases.assets.Where(x => x.name.StartsWith("EmuSAK-win32-x64-") && x.name.EndsWith("portable.zip")).First();

            subProgressLabel.Content = $"Downloading {latestRelease.name}";
            await client.DownloadFile(latestRelease, subProgressBar, $"{installPath}\\Temp\\");
            var filenameWithoutExt = Path.GetFileNameWithoutExtension($"{installPath}\\Temp\\{latestRelease.name}");
            subProgressLabel.Content = $"Unpacking {latestRelease.name} to {installPath}\\Temp\\{filenameWithoutExt}";
           
            ZipFile.ExtractToDirectory($"{installPath}\\Temp\\{latestRelease.name}", $"{installPath}\\Temp\\{filenameWithoutExt}", true);
            //move
            MoveDirectory($"{installPath}\\Temp\\{filenameWithoutExt}\\publish", $"{installPath}\\Tools\\EmuSAK");
            
            //cleanup
            File.Delete($"{installPath}\\Temp\\{latestRelease.name}");

            subProgressLabel.Content = "Finished installing EmuSAK";
        }
    }
}
