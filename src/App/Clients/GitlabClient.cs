using System;
namespace SteamDeckWindows.Clients
{
	public class GitlabClient
	{
		private readonly string GitlabApi = "https://gitlab.com/api/v4/projects/";
		public GithubClient(string owner, string repository)
		{
            //TODO URLEncode owner+repo
            var project = UrlEncode($"{owner}/{repository}");
			GithubApi += $"{GithubApi}/{project}/";
		}

		public async List<PackageDto> GetReleases(){

			using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var result = await client.GetAsync($"{GitlabApi}packages");
			response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<PackageDto>>(await response.Content.ReadAsStringAsync());
		}
		public async List<PackageFileDto> GetRelease(string packageId){

			using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var result = await client.GetAsync($"{GitlabApi}packages/{packageId}/package_files");
			response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<PackageFileDto>>(await response.Content.ReadAsStringAsync());
		}
	}
}

