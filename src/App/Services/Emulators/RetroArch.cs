using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;
using System;
using System.Net.Http;

namespace SteamDeckWindows.Services.Emulators
{
    public class RetroArch : IEmulatorService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath)
        {
            var client = new GithubClient("libretro", "RetroArch");
            var latestRelease = await client.GetLatestRelease();
            var latestReleaseName = latestRelease.name.Replace("v","");
            
            var downloadUrl = $"https://buildbot.libretro.com/stable/{latestReleaseName}/windows/x86_64/RetroArch.7z";

            subProgressLabel.Content = $"Downloading RetroArch {latestRelease.name}";

            using var clientDownload = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);
            System.Threading.CancellationToken cancellationToken = default;
            if (File.Exists($"{installPath}\\Temp\\RetroArch.7z")) File.Delete($"{installPath}\\Temp\\RetroArch.7z");
            using var f = new FileStream($"{installPath}\\Temp\\RetroArch.7z", FileMode.Create, FileAccess.Write, FileShare.None);

            var progress = new Progress<float>();

            //Gets download progress
            progress.ProgressChanged += (sender, value) =>
            {
                subProgressBar.Value = (int)(value * 100);
            };

            await clientDownload.DownloadAsync(downloadUrl, f, progress, cancellationToken);

            //TODO should also download and install cores from latest nightly build?

            subProgressLabel.Content = $"Unpacking RetroArch {latestRelease.name} to {installPath}\\Temp\\RetroArch-{latestRelease.name}";
            ZipFile.ExtractToDirectory($"{installPath}\\Temp\\RetroArch.7z", $"{installPath}\\Temp\\RetroArch-{latestRelease.name}", true);
            //move
            DirectoryExtensions.MoveDirectory($"{installPath}\\Temp\\RetroArch-{latestRelease.name}", $"{installPath}\\RetroArch");
            
            //cleanup
            File.Delete($"{installPath}\\Temp\\RetroArch.7z");

            subProgressLabel.Content = "Finished installing RetroArch";
        }
    }
}
