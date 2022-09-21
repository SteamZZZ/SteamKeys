using Newtonsoft.Json;

namespace SteamKeysApp.Models;

public class Store
{
    [JsonProperty("OSL_SITE_NAME")]  public string Name { get; set; }  
    [JsonProperty("OSL_PRICE"), JsonConverter(typeof(NullToZeroConverter))] public double Price { get; set; }   
    [JsonProperty("OSL_REF")]  public string Link { get; set; }

    public override string ToString() => $"{Name}: {Price}";
}