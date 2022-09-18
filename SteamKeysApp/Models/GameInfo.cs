// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Achievements
{
    public int Total { get; set; }
    public List<Highlighted> Highlighted { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Description { get; set; }
}

public class ContentDescriptors
{
    public List<object> Ids { get; set; }
    public object Notes { get; set; }
}

public class Data
{
    public string Type { get; set; }
    public string Name { get; set; }
    public int SteamAppid { get; set; }
    public int RequiredAge { get; set; }
    public bool IsFree { get; set; }
    public List<int> Dlc { get; set; }
    public string DetailedDescription { get; set; }
    public string AboutTheGame { get; set; }
    public string ShortDescription { get; set; }
    public string SupportedLanguages { get; set; }
    public string HeaderImage { get; set; }
    public string Website { get; set; }
    public PcRequirements PcRequirements { get; set; }
    public MacRequirements MacRequirements { get; set; }
    public LinuxRequirements LinuxRequirements { get; set; }
    public List<string> Developers { get; set; }
    public List<string> Publishers { get; set; }
    public List<object> PackageGroups { get; set; }
    public Platforms Platforms { get; set; }
    public List<Category> Categories { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Screenshot> Screenshots { get; set; }
    public List<Movie> Movies { get; set; }
    public Achievements Achievements { get; set; }
    public ReleaseDate ReleaseDate { get; set; }
    public SupportInfo SupportInfo { get; set; }
    public string Background { get; set; }
    public string BackgroundRaw { get; set; }
    public ContentDescriptors ContentDescriptors { get; set; }
}

public class GameInfo
{
    public bool Success { get; set; }
    public Data Data { get; set; }
}

public class Genre
{
    public string Id { get; set; }
    public string Description { get; set; }
}

public class Highlighted
{
    public string Name { get; set; }
    public string Path { get; set; }
}

public class LinuxRequirements
{
    public string Minimum { get; set; }
    public string Recommended { get; set; }
}

public class MacRequirements
{
    public string Minimum { get; set; }
    public string Recommended { get; set; }
}

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Thumbnail { get; set; }
    public Webm Webm { get; set; }
    public Mp4 Mp4 { get; set; }
    public bool Highlight { get; set; }
}

public class Mp4
{
    public string _480 { get; set; }
    public string Max { get; set; }
}

public class PcRequirements
{
    public string Minimum { get; set; }
    public string Recommended { get; set; }
}

public class Platforms
{
    public bool Windows { get; set; }
    public bool Mac { get; set; }
    public bool Linux { get; set; }
}

public class ReleaseDate
{
    public bool ComingSoon { get; set; }
    public string Date { get; set; }
}

public class Root
{
    public GameInfo GameInfo { get; set; }
}

public class Screenshot
{
    public int Id { get; set; }
    public string PathThumbnail { get; set; }
    public string PathFull { get; set; }
}

public class SupportInfo
{
    public string Url { get; set; }
    public string Email { get; set; }
}

public class Webm
{
    public string _480 { get; set; }
    public string Max { get; set; }
}

