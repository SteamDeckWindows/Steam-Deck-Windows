using SteamDeckWindows.Clients;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;
using SevenZipExtractor;

namespace SteamDeckWindows.Services.Emulators
{
    public class RetroArch : IEmulatorService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath, EmulatorSetting emulatorSetting)
        {
            var client = new GithubClient("libretro", "RetroArch");
            var latestRelease = await client.GetLatestRelease();
            var latestReleaseName = latestRelease.name.Replace("v","");
            
            var downloadUrl = $"https://buildbot.libretro.com/stable/{latestReleaseName}/windows/x86_64/RetroArch.7z";

            subProgressLabel.Content = $"Downloading RetroArch {latestRelease.name}";
            await new GenericHttpDownloadClient().DownloadFile(downloadUrl, "RetroArch.7z", subProgressBar, installPath);

            //TODO should also download and install cores from latest nightly build?

            subProgressLabel.Content = $"Unpacking RetroArch {latestRelease.name} to {installPath}\\Temp\\RetroArch-{latestRelease.name}";
            Directory.CreateDirectory($"{installPath}\\Temp\\RetroArch-{latestRelease.name}");
            using (var archiveFile = new ArchiveFile($"{installPath}\\Temp\\RetroArch.7z"))
            {
                archiveFile.Extract($"{installPath}\\Temp\\RetroArch-{latestRelease.name}"); // extract all
            }

            //move
            DirectoryExtensions.MoveDirectory($"{installPath}\\Temp\\RetroArch-{latestRelease.name}\\RetroArch-Win64", $"{installPath}\\Emulators\\RetroArch");
            
            //cleanup
            File.Delete($"{installPath}\\Temp\\RetroArch.7z");
            if(Directory.Exists($"{installPath}\\Temp\\RetroArch-{latestRelease.name}"))
                Directory.Delete($"{installPath}\\Temp\\RetroArch-{latestRelease.name}");

            subProgressLabel.Content = "Finished installing RetroArch";
        }
    }
}
