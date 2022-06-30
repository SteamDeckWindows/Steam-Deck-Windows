using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;
using SevenZipExtractor;

namespace SteamDeckWindows.Services.Emulators
{
    public class Pcsx2 : IEmulatorService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath, EmulatorSetting emulatorSetting)
        {
            var client = new GithubClient("PCSX2", "pcsx2");
            var latestReleases = await client.GetAllRelease();
            var latest = latestReleases.Where(w => w.prerelease.Equals(true)).OrderByDescending(o => o.created_at).First();
            var assets = await client.GetReleaseAssets(latest.assets_url);
            var latestRelease = assets.Where(x => x.name.EndsWith("windows-64bit-AVX2-Qt.7z")).First();

            subProgressLabel.Content = $"Downloading {latestRelease.name}";
            await client.DownloadFile(latestRelease, subProgressBar, $"{installPath}\\Temp\\");

            subProgressLabel.Content = $"Unpacking {latestRelease.name} to {installPath}\\Temp\\{latestRelease.name}";
            var filenameWithoutExt = Path.GetFileNameWithoutExtension($"{installPath}\\Temp\\{latestRelease.name}");
            Directory.CreateDirectory($"{installPath}\\Temp\\{latestRelease.name}");
            using (var archiveFile = new ArchiveFile($"{installPath}\\Temp\\{latestRelease.name}"))
            {
                archiveFile.Extract($"{installPath}\\Temp\\{filenameWithoutExt}"); // extract all
            }
            //move
            DirectoryExtensions.MoveDirectory($"{installPath}\\Temp\\{filenameWithoutExt}", $"{installPath}\\Emulators\\PCSX2");
            
            //cleanup
            File.Delete($"{installPath}\\Temp\\{latestRelease.name}");
            if (Directory.Exists($"{installPath}\\Temp\\{filenameWithoutExt}"))
                Directory.Delete($"{installPath}\\Temp\\{filenameWithoutExt}");
            subProgressLabel.Content = "Finished installing PCSX2";
        }
    }
}
