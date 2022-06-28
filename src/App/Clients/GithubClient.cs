using Newtonsoft.Json;
using SteamDeckWindows.Clients.Models.Github;
using SteamDeckWindows.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Clients
{
	public class GithubClient
	{
		private readonly string GithubApi = "https://api.github.com/repos";
		public GithubClient(string repository, string channel)
		{
			GithubApi += $"/{repository}/{channel}/";
		}

		public async Task<ReleaseDto> GetLatestRelease(){

			using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
			var url = $"{GithubApi}releases/latest";
			var response = await client.GetAsync(url);
			response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<ReleaseDto>(await response.Content.ReadAsStringAsync());
		}

		public async Task DownloadFile(ReleaseAssetDto asset, ProgressBar progressBar, string savePath)
		{
			using var client = new HttpClient();
			client.Timeout = TimeSpan.FromMinutes(10);
			if (File.Exists($"{savePath}{asset.name}")) File.Delete($"{savePath}{asset.name}");
			using var f = new FileStream($"{savePath}{asset.name}", FileMode.Create, FileAccess.Write, FileShare.None);

			var progress = new Progress<float>();
			System.Threading.CancellationToken cancellationToken = default;

			//Gets download progress
			progress.ProgressChanged += (sender, value) =>
			{
				progressBar.Value = (int)(value * 100);
			};

			await client.DownloadAsync(asset.browser_download_url, f, progress, cancellationToken);

		}
	}
}

