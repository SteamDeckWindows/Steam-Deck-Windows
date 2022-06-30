﻿using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;

namespace SteamDeckWindows.Services.Emulators
{
    public class PrimeHack : IEmulatorService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath, EmulatorSetting emulatorSetting)
        {
            var client = new GithubClient("shiiion", "dolphin");
            var latestReleases = await client.GetLatestRelease();
            var latestRelease = latestReleases.assets.Where(x => x.name.Equals("PrimeHackRelease.zip")).First();

            subProgressLabel.Content = $"Downloading {latestRelease.name}";
            await client.DownloadFile(latestRelease, subProgressBar, $"{installPath}\\Temp\\");

            subProgressLabel.Content = $"Unpacking {latestRelease.name} to {installPath}\\Temp\\{latestRelease.name}";
            var filenameWithoutExt = Path.GetFileNameWithoutExtension($"{installPath}\\Temp\\{latestRelease.name}");
            ZipFile.ExtractToDirectory($"{installPath}\\Temp\\{latestRelease.name}", $"{installPath}\\Temp\\{filenameWithoutExt}", true);
            //move
            DirectoryExtensions.MoveDirectory($"{installPath}\\Temp\\{filenameWithoutExt}", $"{installPath}\\Emulators\\PrimeHack");
            
            //cleanup
            File.Delete($"{installPath}\\Temp\\{latestRelease.name}");
            if (Directory.Exists($"{installPath}\\Temp\\{filenameWithoutExt}"))
                Directory.Delete($"{installPath}\\Temp\\{filenameWithoutExt}");
            subProgressLabel.Content = "Finished installing PrimeHack";
        }
    }
}
