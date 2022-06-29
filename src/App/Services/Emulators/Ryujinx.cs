using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;

namespace SteamDeckWindows.Services.Emulators
{
    public class Ryujinx : IEmulatorService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath)
        {
            var client = new GithubClient("Ryujinx", "release-channel-master");
            var latestReleases = await client.GetLatestRelease();
            var latestRelease = latestReleases.assets.Where(x => x.name.StartsWith("ryujinx") && x.name.EndsWith("win_x64.zip")).First();

            subProgressLabel.Content = $"Downloading {latestRelease.name}";
            await client.DownloadFile(latestRelease, subProgressBar, $"{installPath}\\Temp\\");

            subProgressLabel.Content = $"Unpacking {latestRelease.name} to {installPath}\\Temp\\{latestRelease.name}";
            var filenameWithoutExt = Path.GetFileNameWithoutExtension($"{installPath}\\Temp\\{latestRelease.name}");
            ZipFile.ExtractToDirectory($"{installPath}\\Temp\\{latestRelease.name}", $"{installPath}\\Temp\\{filenameWithoutExt}", true);
            //move
            DirectoryExtensions.MoveDirectory($"{installPath}\\Temp\\{filenameWithoutExt}\\publish", $"{installPath}\\Ryujinx");
            
            //cleanup
            File.Delete($"{installPath}\\Temp\\{latestRelease.name}");

            subProgressLabel.Content = "Finished installing Ryujinx";
        }
    }
}
