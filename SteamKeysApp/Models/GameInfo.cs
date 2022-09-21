// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using Newtonsoft.Json;

public class GameInfo
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("data")]
    public Data Data { get; set; }
}

public class Achievements
{
    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("highlighted")]
    public List<Highlighted> Highlighted { get; set; }
}

public class Category
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}

public class ContentDescriptors
{
    [JsonProperty("ids")]
    public List<object> Ids { get; set; }

    [JsonProperty("notes")]
    public object Notes { get; set; }
}

public class Data
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("steam_appid")]
    public int SteamAppid { get; set; }

    [JsonProperty("required_age")]
    public int RequiredAge { get; set; }

    [JsonProperty("is_free")]
    public bool IsFree { get; set; }

    [JsonProperty("dlc")]
    public List<int> Dlc { get; set; }

    [JsonProperty("detailed_description")]
    public string DetailedDescription { get; set; }

    [JsonProperty("about_the_game")]
    public string AboutTheGame { get; set; }

    [JsonProperty("short_description")]
    public string ShortDescription { get; set; }

    [JsonProperty("supported_languages")]
    public string SupportedLanguages { get; set; }

    [JsonProperty("header_image")]
    public string HeaderImage { get; set; }

    [JsonProperty("website")]
    public string Website { get; set; }

    //[JsonProperty("pc_requirements")]
    //public PcRequirements PcRequirements { get; set; }

    //[JsonProperty("mac_requirements")]
    //public MacRequirements[] MacRequirements { get; set; }

    //[JsonProperty("linux_requirements")]
    //public LinuxRequirements[] LinuxRequirements { get; set; }

    [JsonProperty("developers")]
    public List<string> Developers { get; set; }

    [JsonProperty("publishers")]
    public List<string> Publishers { get; set; }

    [JsonProperty("package_groups")]
    public List<object> PackageGroups { get; set; }

    [JsonProperty("platforms")]
    public Platforms Platforms { get; set; }

    [JsonProperty("categories")]
    public List<Category> Categories { get; set; }

    [JsonProperty("genres")]
    public List<Genre> Genres { get; set; }

    [JsonProperty("screenshots")]
    public List<Screenshot> Screenshots { get; set; }

    [JsonProperty("movies")]
    public List<Movie> Movies { get; set; }

    [JsonProperty("achievements")]
    public Achievements Achievements { get; set; }

    [JsonProperty("release_date")]
    public ReleaseDate ReleaseDate { get; set; }

    [JsonProperty("support_info")]
    public SupportInfo SupportInfo { get; set; }

    [JsonProperty("background")]
    public string Background { get; set; }

    [JsonProperty("background_raw")]
    public string BackgroundRaw { get; set; }

    [JsonProperty("content_descriptors")]
    public ContentDescriptors ContentDescriptors { get; set; }
}

public class Genre
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}

public class Highlighted
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("path")]
    public string Path { get; set; }
}

public class LinuxRequirements
{
    [JsonProperty("minimum")]
    public string Minimum { get; set; }

    [JsonProperty("recommended")]
    public string Recommended { get; set; }
}

public class MacRequirements
{
    [JsonProperty("minimum")]
    public string Minimum { get; set; }

    [JsonProperty("recommended")]
    public string Recommended { get; set; }
}

public class Movie
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("thumbnail")]
    public string Thumbnail { get; set; }

    [JsonProperty("webm")]
    public Webm Webm { get; set; }

    [JsonProperty("mp4")]
    public Mp4 Mp4 { get; set; }

    [JsonProperty("highlight")]
    public bool Highlight { get; set; }
}

public class Mp4
{
    [JsonProperty("480")]
    public string _480 { get; set; }

    [JsonProperty("max")]
    public string Max { get; set; }
}

public class PcRequirements
{
    [JsonProperty("minimum")]
    public string Minimum { get; set; }

    [JsonProperty("recommended")]
    public string Recommended { get; set; }
}

public class Platforms
{
    [JsonProperty("windows")]
    public bool Windows { get; set; }

    [JsonProperty("mac")]
    public bool Mac { get; set; }

    [JsonProperty("linux")]
    public bool Linux { get; set; }
}

public class ReleaseDate
{
    [JsonProperty("coming_soon")]
    public bool ComingSoon { get; set; }

    [JsonProperty("date")]
    public string Date { get; set; }
}

public class Root
{
    [JsonProperty("GameInfo")]
    public GameInfo GameInfo { get; set; }
}

public class Screenshot
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("path_thumbnail")]
    public string PathThumbnail { get; set; }

    [JsonProperty("path_full")]
    public string PathFull { get; set; }
}

public class SupportInfo
{
    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }
}

public class Webm
{
    [JsonProperty("480")]
    public string _480 { get; set; }

    [JsonProperty("max")]
    public string Max { get; set; }
}

