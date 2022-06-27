using System;
using System.Collections.Generic;

namespace SteamDeckWindows.Clients.Models.Gitlab
{
    public class PackageLinks
    {
        public string web_path { get; set; }
    }

    public class PackageDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string version { get; set; }
        public string package_type { get; set; }
        public string status { get; set; }
        public PackageLinks _links { get; set; }
        public DateTime created_at { get; set; }
        public List<object> tags { get; set; }
    }
}
