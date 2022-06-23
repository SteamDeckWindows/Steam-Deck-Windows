using Newtonsoft.Json;
using SteamDeckWindows.Clients.Models.Gitlab;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SteamDeckWindows.Clients
{
	public class GitlabClient
	{
		private readonly string GitlabApi = "https://gitlab.com/api/v4/projects/";
		public GitlabClient(string owner, string repository)
		{
            //TODO URLEncode owner+repo
            var project = WebUtility.UrlEncode($"{owner}/{repository}");
			GitlabApi += $"{GitlabApi}/{project}/";
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
	}
}

