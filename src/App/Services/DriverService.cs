using SteamDeckWindows.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Services
{
    public class DriverService : IDriverService
    {
        private readonly string driversUrl = @"https://steamdeck-packages.steamos.cloud/misc/windows/drivers/";
        private readonly string savePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SteamDeckWindows\\drivers\\";

        private readonly List<string> files = new List<string>() {
            //"APU_220520a-377788E-2206021014.zip", //APU driver
            //"Aerith Windows Driver_2204252254.zip",  //Unknown
            "BayHub_SD_STOR_installV3.4.01.89_W10W11_logoed_20220228.zip", //SD Card reader
            "RTBlueR_FilterDriver_1041.3005_1201.2021_new_L.zip", //Bluetooth
            "RTLWlanE_WindowsDriver_2024.0.10.137_Drv_3.00.0039_Win11.L.zip", //Wi-Fi driver
            "cs35l41_062022.zip", // Audio 1
            "NAU88L21_x64_1.0.6.0_WHQL - DUA_BIQ_WHQL.zip" //Audio 2
        };

        public async Task DownloadDrivers(ProgressBar progressBar, ProgressBar subProgressBar, Label progressLabel, Label subProgressLabel)
        {
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

            for (var i = 0; i < files.Count(); i++)
            {
                var file = files[i];
                subProgressLabel.Content = "Downloading file " + (i+1).ToString() + " of " + files.Count().ToString() + " filename - " + file;
                await GetFile(file, subProgressBar);
            }
        }

        private async Task GetFile(string file, ProgressBar progressBar)
        {
            // Seting up the http client used to download the data
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);

            using var f = new FileStream(savePath + file, FileMode.Create, FileAccess.Write, FileShare.None);

            Progress<float> progress = new Progress<float>();
            System.Threading.CancellationToken cancellationToken = default;

            //Gets download progress
            progress.ProgressChanged += (sender, value) =>
            {
                progressBar.Value = (int)(value * 100);
            };

            await client.DownloadAsync(driversUrl + file, f, progress, cancellationToken);
        }
    }
}
