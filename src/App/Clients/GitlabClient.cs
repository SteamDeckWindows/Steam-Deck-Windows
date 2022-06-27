using Newtonsoft.Json;
using SteamDeckWindows.Clients.Models.Gitlab;
using SteamDeckWindows.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Clients
{
	public class GitlabClient
	{
		private readonly string GitlabApi = "https://gitlab.com/api/v4/projects/";
		private readonly string GitLabDownloadPath = "https://gitlab.com/";

		public GitlabClient(string owner, string repository)
		{
            //TODO URLEncode owner+repo
            var project = WebUtility.UrlEncode($"{owner}/{repository}");
			GitlabApi += $"{project}/";
			GitLabDownloadPath += $"{owner}/{repository}/-/package_files/";
		}

		public async Task<List<PackageDto>> GetReleases(){

			using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await client.GetAsync($"{GitlabApi}packages");
			response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<PackageDto>>(await response.Content.ReadAsStringAsync());
		}
		public async Task<List<PackageFileDto>> GetRelease(string packageId){

			using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await client.GetAsync($"{GitlabApi}packages/{packageId}/package_files");
			response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<PackageFileDto>>(await response.Content.ReadAsStringAsync());
		}
		public async Task DownloadFile(PackageFileDto fileDto, ProgressBar progressBar, string savePath)
        {
			Directory.CreateDirectory($"{savePath}");
			using var client = new HttpClient();
			client.Timeout = TimeSpan.FromMinutes(10);

			var downloadUrl = $"{GitLabDownloadPath}{fileDto.id}/download";

			using var f = new FileStream($"{savePath}{fileDto.file_name}", FileMode.Create, FileAccess.Write, FileShare.None);

			var progress = new Progress<float>();
			System.Threading.CancellationToken cancellationToken = default;

			//Gets download progress
			progress.ProgressChanged += (sender, value) =>
			{
				progressBar.Value = (int)(value * 100);
			};

			await client.DownloadAsync(downloadUrl, f, progress, cancellationToken);

		}
	}
}

