using System;
using System.Collections.Generic;

namespace WebSteamParser.Models
{
    public partial class UserList
    {
        public int UlId { get; set; }
        public string? UlLogin { get; set; }
        public string? UlPassword { get; set; }
        public DateTime? UlLastVisit { get; set; }
        public double? UlBalance { get; set; }
    }
}
