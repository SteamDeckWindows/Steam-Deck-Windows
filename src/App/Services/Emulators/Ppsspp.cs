using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;
using SteamDeckWindows.Models;

namespace SteamDeckWindows.Services.Emulators
{
    public class Ppsspp : IEmulatorService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath, EmulatorSetting emulatorSetting)
        {
            //https://ppsspp.org/files/1_12_3/ppsspp_win.zip
            var client = new GenericHttpDownloadClient();
            //var latestReleases = await client.GetLatestRelease();
            var latestRelease = $"https://ppsspp.org/files/{emulatorSetting.RecommendedVersion}/ppsspp_win.zip";

            subProgressLabel.Content = $"Downloading PPSSPP {emulatorSetting.RecommendedVersion}";
            await client.DownloadFile(latestRelease, $"{emulatorSetting.RecommendedVersion}_ppsspp_win.zip", subProgressBar, $"{installPath}\\Temp\\");

            subProgressLabel.Content = $"Unpacking {emulatorSetting.RecommendedVersion}_ppsspp_win.zip to {installPath}\\Temp\\PPSSPP";
            var filenameWithoutExt = Path.GetFileNameWithoutExtension($"{installPath}\\Temp\\{emulatorSetting.RecommendedVersion}_ppsspp_win.zip");
            ZipFile.ExtractToDirectory($"{installPath}\\Temp\\{emulatorSetting.RecommendedVersion}_ppsspp_win.zip", $"{installPath}\\Temp\\PPSSPP", true);
            //move
            DirectoryExtensions.MoveDirectory($"{installPath}\\Temp\\PPSSPP", $"{installPath}\\Emulators\\PPSSPP");
            
            //cleanup
            File.Delete($"{installPath}\\Temp\\{emulatorSetting.RecommendedVersion}_ppsspp_win.zip");
            if (Directory.Exists($"{installPath}\\Emulators\\PPSSPP"))
                Directory.Delete($"{installPath}\\Emulators\\PPSSPP");
            subProgressLabel.Content = "Finished installing PPSSPP";
        }
    }
}
