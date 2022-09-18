namespace SteamKeysApp.Models
{
    public class Game
    {
        public int SteamId { get; set; }
        public int SteamBundleId { get; set; }
        public string Name { get; set; }
        public string SteamRef => $"https://store.steampowered.com/app/{SteamId}";
        public string Image => $"https://cdn.akamai.steamstatic.com/steam/apps/{SteamId}/header.jpg";
        public string ImageBackground => $"https://cdn.akamai.steamstatic.com/steam/apps/{SteamId}/page_bg_generated_v6b.jpg";

        public bool IsAvaliable { get; set; }
        
        public float PriceDollar { get; set; }
        public float PriceRu { get; set; }
        public float PriceKz { get; set; }
        public float PriceTr { get; set; }


        public Dictionary<string,float> Prices = new Dictionary<string, float>() { { "SteamBuy", 0 } };
        public string CheapestStore => Prices.MinBy(p => p.Value).Key;
        public float LowestPrice => Prices.MinBy(p => p.Value).Value;
    }
}
