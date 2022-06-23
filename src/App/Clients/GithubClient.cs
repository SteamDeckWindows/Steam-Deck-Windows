using System;
namespace SteamDeckWindows.Clients
{
	public class GithubClient
	{
		private readonly string GithubApi = "https://api.github.com/repos";
		public GithubClient(string owner, string repository)
		{
			GithubApi += $"{GithubApi}/{owner}/{repository}/";
		}

		public async List<ReleaseDto> GetReleases(){

			using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var result = await client.GetAsync($"{GithubApi}releases");
			response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<ReleaseDto>>(await response.Content.ReadAsStringAsync());
		}
		public async ReleaseDto GetLatestRelease(){

			using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var result = await client.GetAsync($"{GithubApi}releases/latest");
			response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<ReleaseDto>(await response.Content.ReadAsStringAsync());
		}
	}
}

