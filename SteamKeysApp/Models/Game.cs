using Newtonsoft.Json;

namespace SteamKeysApp.Models
{
    public class Game
    {
        [JsonProperty("GL_STEAM_ID")] public int SteamId { get; set; }
        [JsonProperty("GL_NAME")] public string Name { get; set; }
        [JsonProperty("GL_AVAILABILITY")] public bool IsAvaliable { get; set; }
        [JsonProperty("GL_PRICE"), JsonConverter(typeof(NullToZeroConverter))] public double PriceUm { get; set; }
        [JsonProperty("GL_PRICE_RU"), JsonConverter(typeof(NullToZeroConverter))] public double PriceRu { get; set; }
        [JsonProperty("GL_PRICE_KZ"), JsonConverter(typeof(NullToZeroConverter))] public double PriceKz { get; set; }
        [JsonProperty("GL_PRICE_TR"), JsonConverter(typeof(NullToZeroConverter))] public double PriceTr { get; set; }
        [JsonProperty("dbo.OTHER_SITE_LIST")] public List<Store> Stores { get; set; } 

        public string SteamRef => $"https://store.steampowered.com/app/{SteamId}";
        public string Image => $"https://cdn.akamai.steamstatic.com/steam/apps/{SteamId}/header.jpg";
        public string ImageBackground => $"https://cdn.akamai.steamstatic.com/steam/apps/{SteamId}/page_bg_generated_v6b.jpg";
        public Store CheapestStore => Stores?.MinBy(p => p.Price);

        public double? StoreLowestPrice => CheapestStore?.Price;

        public override string ToString()
        {
            return $"{SteamId} {Name} Is-{IsAvaliable} ${PriceUm} Rub{PriceRu} Kz{PriceKz} Tr{PriceTr}";
        }
    }
}