using SteamDeckWindows.Extensions;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Clients
{
    public class GenericHttpDownloadClient
    {
        public async Task DownloadFile(string downloadUrl, string filename, ProgressBar progressBar, string installPath)
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);
            System.Threading.CancellationToken cancellationToken = default;
            if (File.Exists($"{installPath}\\Temp\\{filename}")) File.Delete($"{installPath}\\Temp\\{filename}");
            using var f = new FileStream($"{installPath}\\Temp\\{filename}", FileMode.Create, FileAccess.Write, FileShare.None);

            var progress = new Progress<float>();

            //Gets download progress
            progress.ProgressChanged += (sender, value) =>
            {
                progressBar.Value = (int)(value * 100);
            };

            await client.DownloadAsync(downloadUrl, f, progress, cancellationToken);

        }
    }
}
