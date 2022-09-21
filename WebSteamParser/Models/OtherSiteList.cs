using System;
using System.Collections.Generic;

namespace WebSteamParser.Models
{
    public partial class OtherSiteList
    {
        public int OslId { get; set; }
        public string? OslSiteName { get; set; }
        public string? OslName { get; set; }
        public double? OslPrice { get; set; }
        public string? OslRef { get; set; }
        public int? OslSteamId { get; set; }
    }
}
