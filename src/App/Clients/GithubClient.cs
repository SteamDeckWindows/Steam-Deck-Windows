using Newtonsoft.Json;
using SteamDeckWindows.Clients.Models.Github;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SteamDeckWindows.Clients
{
	public class GithubClient
	{
		private readonly string GithubApi = "https://api.github.com/repos";
		public GithubClient(string owner, string repository)
		{
			GithubApi += $"{GithubApi}/{owner}/{repository}/";
		}

		public async Task<List<ReleaseDto>> GetReleases(){

			using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await client.GetAsync($"{GithubApi}releases");
			response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<ReleaseDto>>(await response.Content.ReadAsStringAsync());
		}
		public async Task<ReleaseDto> GetLatestRelease(){

			using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var response = await client.GetAsync($"{GithubApi}releases/latest");
			response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<ReleaseDto>(await response.Content.ReadAsStringAsync());
		}
	}
}

