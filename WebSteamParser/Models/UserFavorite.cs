using System;
using System.Collections.Generic;

namespace WebSteamParser.Models
{
    public partial class UserFavorite
    {
        public int UfId { get; set; }
        public int? UfUlId { get; set; }
        public int? UfGlSteamId { get; set; }
    }
}
