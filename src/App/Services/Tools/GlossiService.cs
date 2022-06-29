using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;
using System.Net.Http;
using System;

namespace SteamDeckWindows.Services.Tools
{
    public class GlosSIService : IToolsService
    {
        //public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath)
        //{
        //    //Todo investigate if we can use appveyyor api to find latest
        //    var downloadUrl = @"https://ci.appveyor.com/api/buildjobs/4bn7vm501nwecr48/artifacts/x64%2FRelease%2FGlosSI-snapshot.zip";
        //    using var client = new HttpClient();
        //    client.Timeout = TimeSpan.FromMinutes(10);
        //    System.Threading.CancellationToken cancellationToken = default;
        //    if (File.Exists($"{installPath}\\Temp\\GlosSI-snapshot.zip")) File.Delete($"{installPath}\\Temp\\GlosSI-snapshot.zip");
        //    using var f = new FileStream($"{installPath}\\Temp\\GlosSI-snapshot.zip", FileMode.Create, FileAccess.Write, FileShare.None);

        //    //var progress = new Progress<float>();

        //    //Gets download progress
        //    progress.ProgressChanged += (sender, value) =>
        //    {
        //        subProgressBar.Value = (int)(value * 100);
        //    };

        //    await client.DownloadAsync(downloadUrl, f, progress, cancellationToken);


        //    var filenameWithoutExt = Path.GetFileNameWithoutExtension($"{installPath}\\Temp\\GlosSI-snapshot.zip");
        //    subProgressLabel.Content = $"Unpacking GlosSI-snapshot.zip to {installPath}\\Temp\\{filenameWithoutExt}";

        //    ZipFile.ExtractToDirectory($"{installPath}\\Temp\\GlosSI-snapshot.zip", $"{installPath}\\Temp\\{filenameWithoutExt}", true);
        //    //move
        //    DirectoryExtensions.MoveDirectory($"{installPath}\\Temp\\{filenameWithoutExt}", $"{installPath}\\Tools\\GlosSI");

        //    //cleanup
        //    File.Delete($"{installPath}\\Temp\\{filenameWithoutExt}");

        //    subProgressLabel.Content = "Finished installing GlosSI";
        //}
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath)
        {
            var client = new GithubClient("Alia5", "GlosSI");
            var latestReleases = await client.GetAllRelease();
            var latest = latestReleases.Where(w => w.prerelease.Equals(true)).OrderByDescending(o => o.created_at).First();
            var assets = await client.GetReleaseAssets(latest.assets_url);
            var latestRelease = assets.Where(x => x.name.StartsWith("GlosSI-") && x.name.EndsWith(".zip")).First();

            subProgressLabel.Content = $"Downloading {latestRelease.name}";
            await client.DownloadFile(latestRelease, subProgressBar, $"{installPath}\\Temp\\");
            var filenameWithoutExt = Path.GetFileNameWithoutExtension($"{installPath}\\Temp\\{latestRelease.name}");
            subProgressLabel.Content = $"Unpacking {latestRelease.name} to {installPath}\\Temp\\{filenameWithoutExt}";

            ZipFile.ExtractToDirectory($"{installPath}\\Temp\\{latestRelease.name}", $"{installPath}\\Temp\\{filenameWithoutExt}", true);
            //move
            DirectoryExtensions.MoveDirectory($"{installPath}\\Temp\\{filenameWithoutExt}", $"{installPath}\\Tools\\GlosSI");

            //cleanup
            File.Delete($"{installPath}\\Temp\\{latestRelease.name}");

            subProgressLabel.Content = "Finished installing GlosSI";
        }
    }
}
