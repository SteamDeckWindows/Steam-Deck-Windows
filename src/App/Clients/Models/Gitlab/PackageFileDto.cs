namespace SteamDeckWindows.Clients.Models.Gitlab
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PackageFileDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("package_id")]
        public long PackageId { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("file_md5")]
        public object FileMd5 { get; set; }

        [JsonProperty("file_sha1")]
        public object FileSha1 { get; set; }

        [JsonProperty("file_sha256")]
        public string FileSha256 { get; set; }
    }
}
