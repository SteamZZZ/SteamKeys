using System;
using System.Collections.Generic;

namespace WebSteamParser.Models
{
    public partial class GameList
    {
        public int GlId { get; set; }
        public string GlName { get; set; } = null!;
        public double GlPrice { get; set; }
        public bool? GlAvailability { get; set; }
        public double? GlPriceRu { get; set; }
        public double? GlPriceKz { get; set; }
        public double? GlPriceTr { get; set; }
        public int? GlSteamId { get; set; }
    }
}
