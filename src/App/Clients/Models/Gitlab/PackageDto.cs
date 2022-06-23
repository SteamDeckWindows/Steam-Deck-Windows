namespace SteamDeckWindows.Clients.Models.Gitlab
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PackageDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("package_type")]
        public PackageType PackageType { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("tags")]
        public object[] Tags { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("web_path")]
        public string WebPath { get; set; }
    }

    public enum Name { Emulators, EsDePrerelease, EsDeStable };

    public enum PackageType { Generic };

    public enum Status { Default };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                NameConverter.Singleton,
                PackageTypeConverter.Singleton,
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class NameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Name) || t == typeof(Name?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "ES-DE_Prerelease":
                    return Name.EsDePrerelease;
                case "ES-DE_Stable":
                    return Name.EsDeStable;
                case "Emulators":
                    return Name.Emulators;
            }
            throw new Exception("Cannot unmarshal type Name");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Name)untypedValue;
            switch (value)
            {
                case Name.EsDePrerelease:
                    serializer.Serialize(writer, "ES-DE_Prerelease");
                    return;
                case Name.EsDeStable:
                    serializer.Serialize(writer, "ES-DE_Stable");
                    return;
                case Name.Emulators:
                    serializer.Serialize(writer, "Emulators");
                    return;
            }
            throw new Exception("Cannot marshal type Name");
        }

        public static readonly NameConverter Singleton = new NameConverter();
    }

    internal class PackageTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PackageType) || t == typeof(PackageType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "generic")
            {
                return PackageType.Generic;
            }
            throw new Exception("Cannot unmarshal type PackageType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PackageType)untypedValue;
            if (value == PackageType.Generic)
            {
                serializer.Serialize(writer, "generic");
                return;
            }
            throw new Exception("Cannot marshal type PackageType");
        }

        public static readonly PackageTypeConverter Singleton = new PackageTypeConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "default")
            {
                return Status.Default;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            if (value == Status.Default)
            {
                serializer.Serialize(writer, "default");
                return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
