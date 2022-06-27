using System;
namespace SteamDeckWindows.Clients.Models.Gitlab
{
    public class PackageFileDto
    {
        public int id { get; set; }
        public int package_id { get; set; }
        public DateTime created_at { get; set; }
        public string file_name { get; set; }
        public int size { get; set; }
        public string file_md5 { get; set; }
        public string file_sha1 { get; set; }
        public string file_sha256 { get; set; }
    }
}
