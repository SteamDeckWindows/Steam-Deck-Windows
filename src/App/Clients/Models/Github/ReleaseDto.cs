namespace SteamDeckWindows.Clients.Models.Github
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ReleaseDto
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("assets_url")]
        public Uri AssetsUrl { get; set; }

        [JsonProperty("upload_url")]
        public string UploadUrl { get; set; }

        [JsonProperty("html_url")]
        public Uri HtmlUrl { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("node_id")]
        public string NodeId { get; set; }

        [JsonProperty("tag_name")]
        public string TagName { get; set; }

        [JsonProperty("target_commitish")]
        public TargetCommitish TargetCommitish { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("draft")]
        public bool Draft { get; set; }

        [JsonProperty("prerelease")]
        public bool Prerelease { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("published_at")]
        public DateTimeOffset PublishedAt { get; set; }

        [JsonProperty("assets")]
        public Asset[] Assets { get; set; }

        [JsonProperty("tarball_url")]
        public Uri TarballUrl { get; set; }

        [JsonProperty("zipball_url")]
        public Uri ZipballUrl { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("reactions", NullValueHandling = NullValueHandling.Ignore)]
        public Reactions Reactions { get; set; }
    }

    public partial class Asset
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("node_id")]
        public string NodeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("uploader")]
        public Author Uploader { get; set; }

        [JsonProperty("content_type")]
        public ContentType ContentType { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("download_count")]
        public long DownloadCount { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("browser_download_url")]
        public Uri BrowserDownloadUrl { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("login")]
        public Login Login { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("node_id")]
        public NodeId NodeId { get; set; }

        [JsonProperty("avatar_url")]
        public Uri AvatarUrl { get; set; }

        [JsonProperty("gravatar_id")]
        public string GravatarId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("html_url")]
        public Uri HtmlUrl { get; set; }

        [JsonProperty("followers_url")]
        public Uri FollowersUrl { get; set; }

        [JsonProperty("following_url")]
        public FollowingUrl FollowingUrl { get; set; }

        [JsonProperty("gists_url")]
        public GistsUrl GistsUrl { get; set; }

        [JsonProperty("starred_url")]
        public StarredUrl StarredUrl { get; set; }

        [JsonProperty("subscriptions_url")]
        public Uri SubscriptionsUrl { get; set; }

        [JsonProperty("organizations_url")]
        public Uri OrganizationsUrl { get; set; }

        [JsonProperty("repos_url")]
        public Uri ReposUrl { get; set; }

        [JsonProperty("events_url")]
        public EventsUrl EventsUrl { get; set; }

        [JsonProperty("received_events_url")]
        public Uri ReceivedEventsUrl { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("site_admin")]
        public bool SiteAdmin { get; set; }
    }

    public partial class Reactions
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("+1")]
        public long The1 { get; set; }

        [JsonProperty("-1")]
        public long Reactions1 { get; set; }

        [JsonProperty("laugh")]
        public long Laugh { get; set; }

        [JsonProperty("hooray")]
        public long Hooray { get; set; }

        [JsonProperty("confused")]
        public long Confused { get; set; }

        [JsonProperty("heart")]
        public long Heart { get; set; }

        [JsonProperty("rocket")]
        public long Rocket { get; set; }

        [JsonProperty("eyes")]
        public long Eyes { get; set; }
    }

    public enum ContentType { Raw };

    public enum State { Uploaded };

    public enum EventsUrl { HttpsApiGithubComUsersRyujinxBotEventsPrivacy };

    public enum FollowingUrl { HttpsApiGithubComUsersRyujinxBotFollowingOtherUser };

    public enum GistsUrl { HttpsApiGithubComUsersRyujinxBotGistsGistId };

    public enum Login { RyujinxBot };

    public enum NodeId { Mdq6VxNlcjYxMti3NjQ1 };

    public enum StarredUrl { HttpsApiGithubComUsersRyujinxBotStarredOwnerRepo };

    public enum TypeEnum { User };

    public enum TargetCommitish { Master };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ContentTypeConverter.Singleton,
                StateConverter.Singleton,
                EventsUrlConverter.Singleton,
                FollowingUrlConverter.Singleton,
                GistsUrlConverter.Singleton,
                LoginConverter.Singleton,
                NodeIdConverter.Singleton,
                StarredUrlConverter.Singleton,
                TypeEnumConverter.Singleton,
                TargetCommitishConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ContentTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ContentType) || t == typeof(ContentType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "raw")
            {
                return ContentType.Raw;
            }
            throw new Exception("Cannot unmarshal type ContentType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ContentType)untypedValue;
            if (value == ContentType.Raw)
            {
                serializer.Serialize(writer, "raw");
                return;
            }
            throw new Exception("Cannot marshal type ContentType");
        }

        public static readonly ContentTypeConverter Singleton = new ContentTypeConverter();
    }

    internal class StateConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(State) || t == typeof(State?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "uploaded")
            {
                return State.Uploaded;
            }
            throw new Exception("Cannot unmarshal type State");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (State)untypedValue;
            if (value == State.Uploaded)
            {
                serializer.Serialize(writer, "uploaded");
                return;
            }
            throw new Exception("Cannot marshal type State");
        }

        public static readonly StateConverter Singleton = new StateConverter();
    }

    internal class EventsUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EventsUrl) || t == typeof(EventsUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/RyujinxBot/events{/privacy}")
            {
                return EventsUrl.HttpsApiGithubComUsersRyujinxBotEventsPrivacy;
            }
            throw new Exception("Cannot unmarshal type EventsUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (EventsUrl)untypedValue;
            if (value == EventsUrl.HttpsApiGithubComUsersRyujinxBotEventsPrivacy)
            {
                serializer.Serialize(writer, "https://api.github.com/users/RyujinxBot/events{/privacy}");
                return;
            }
            throw new Exception("Cannot marshal type EventsUrl");
        }

        public static readonly EventsUrlConverter Singleton = new EventsUrlConverter();
    }

    internal class FollowingUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FollowingUrl) || t == typeof(FollowingUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/RyujinxBot/following{/other_user}")
            {
                return FollowingUrl.HttpsApiGithubComUsersRyujinxBotFollowingOtherUser;
            }
            throw new Exception("Cannot unmarshal type FollowingUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FollowingUrl)untypedValue;
            if (value == FollowingUrl.HttpsApiGithubComUsersRyujinxBotFollowingOtherUser)
            {
                serializer.Serialize(writer, "https://api.github.com/users/RyujinxBot/following{/other_user}");
                return;
            }
            throw new Exception("Cannot marshal type FollowingUrl");
        }

        public static readonly FollowingUrlConverter Singleton = new FollowingUrlConverter();
    }

    internal class GistsUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GistsUrl) || t == typeof(GistsUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/RyujinxBot/gists{/gist_id}")
            {
                return GistsUrl.HttpsApiGithubComUsersRyujinxBotGistsGistId;
            }
            throw new Exception("Cannot unmarshal type GistsUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (GistsUrl)untypedValue;
            if (value == GistsUrl.HttpsApiGithubComUsersRyujinxBotGistsGistId)
            {
                serializer.Serialize(writer, "https://api.github.com/users/RyujinxBot/gists{/gist_id}");
                return;
            }
            throw new Exception("Cannot marshal type GistsUrl");
        }

        public static readonly GistsUrlConverter Singleton = new GistsUrlConverter();
    }

    internal class LoginConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Login) || t == typeof(Login?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "RyujinxBot")
            {
                return Login.RyujinxBot;
            }
            throw new Exception("Cannot unmarshal type Login");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Login)untypedValue;
            if (value == Login.RyujinxBot)
            {
                serializer.Serialize(writer, "RyujinxBot");
                return;
            }
            throw new Exception("Cannot marshal type Login");
        }

        public static readonly LoginConverter Singleton = new LoginConverter();
    }

    internal class NodeIdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(NodeId) || t == typeof(NodeId?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "MDQ6VXNlcjYxMTI3NjQ1")
            {
                return NodeId.Mdq6VxNlcjYxMti3NjQ1;
            }
            throw new Exception("Cannot unmarshal type NodeId");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (NodeId)untypedValue;
            if (value == NodeId.Mdq6VxNlcjYxMti3NjQ1)
            {
                serializer.Serialize(writer, "MDQ6VXNlcjYxMTI3NjQ1");
                return;
            }
            throw new Exception("Cannot marshal type NodeId");
        }

        public static readonly NodeIdConverter Singleton = new NodeIdConverter();
    }

    internal class StarredUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(StarredUrl) || t == typeof(StarredUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/RyujinxBot/starred{/owner}{/repo}")
            {
                return StarredUrl.HttpsApiGithubComUsersRyujinxBotStarredOwnerRepo;
            }
            throw new Exception("Cannot unmarshal type StarredUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (StarredUrl)untypedValue;
            if (value == StarredUrl.HttpsApiGithubComUsersRyujinxBotStarredOwnerRepo)
            {
                serializer.Serialize(writer, "https://api.github.com/users/RyujinxBot/starred{/owner}{/repo}");
                return;
            }
            throw new Exception("Cannot marshal type StarredUrl");
        }

        public static readonly StarredUrlConverter Singleton = new StarredUrlConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "User")
            {
                return TypeEnum.User;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.User)
            {
                serializer.Serialize(writer, "User");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class TargetCommitishConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TargetCommitish) || t == typeof(TargetCommitish?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "master")
            {
                return TargetCommitish.Master;
            }
            throw new Exception("Cannot unmarshal type TargetCommitish");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TargetCommitish)untypedValue;
            if (value == TargetCommitish.Master)
            {
                serializer.Serialize(writer, "master");
                return;
            }
            throw new Exception("Cannot marshal type TargetCommitish");
        }

        public static readonly TargetCommitishConverter Singleton = new TargetCommitishConverter();
    }
}
