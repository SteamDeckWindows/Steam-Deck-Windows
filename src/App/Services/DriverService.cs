using SteamDeckWindows.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Services
{
    public class DriverService
    {
        private readonly string driversUrl = @"https://steamdeck-packages.steamos.cloud/misc/windows/drivers/";

        private readonly List<string> files = new() {
            "APU_220520a-377788E-2206021014.zip", //APU driver                              2022-Jun-16 23:36
            //"Aerith Windows Driver_2204252254.zip",  //APU Driver 	                    2022-May-13 19:12
            "BayHub_SD_STOR_installV3.4.01.89_W10W11_logoed_20220228.zip", //SD Card reader 2022-Mar-15 00:04
            "RTBlueR_FilterDriver_1041.3005_1201.2021_new_L.zip", //Bluetooth               2022-Mar-09 20:07
            "RTLWlanE_WindowsDriver_2024.0.10.137_Drv_3.00.0039_Win11.L.zip", //WiFi driver 2022-Mar-09 20:07
            "cs35l41_062022.zip", // Audio 1                                                2022-Jun-13 23:52
            "NAU88L21_x64_1.0.6.0_WHQL - DUA_BIQ_WHQL.zip" //Audio 2                        2022-May-13 19:12
        };

        public async Task DownloadDrivers(ProgressBar subProgressBar, Label subProgressLabel, string installPath)
        {
            if (!Directory.Exists($"{installPath}\\Drivers")) Directory.CreateDirectory($"{installPath}\\Drivers");

            for (var i = 0; i < files.Count(); i++)
            {
                var file = files[i];
                subProgressLabel.Content = "Downloading file " + (i+1).ToString() + " of " + files.Count().ToString() + " filename - " + file;
                await GetFile(file, subProgressBar, installPath);
                ZipFile.ExtractToDirectory($"{installPath}\\Temp\\{file}", $"{installPath}\\Drivers\\", true);
                File.Delete($"{installPath}\\Temp\\{file}");
            }
        }

        private async Task GetFile(string file, ProgressBar progressBar, string installPath)
        {
            // Seting up the http client used to download the data
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);

            using var f = new FileStream($"{installPath}\\Temp\\{file}", FileMode.Create, FileAccess.Write, FileShare.None);

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
