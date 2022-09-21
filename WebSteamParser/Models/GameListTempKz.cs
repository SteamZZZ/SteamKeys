using System;
using System.Collections.Generic;

namespace WebSteamParser.Models
{
    public partial class GameListTempKz
    {
        public int GltId { get; set; }
        public double? GltPrice { get; set; }
        public int? GltSteamId { get; set; }
        public string? GltImagePath { get; set; }
    }
}
