using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamExperiments
{
    internal class Game
    {
        public int Id { get; set; }
        public int SteamId { get; set; }
        public int SteamBundleId { get; set; }

        public string Name { get; set; }
        public int SteamRef { get; set; }
        public string ImagePath { get; set; }
        public bool IsAvaliable { get; set; }

        public float PriceDollar { get; set; }
        public float PriceRu { get; set; }
        public float PriceKz { get; set; }
        public float PriceTr { get; set; }
    }
}
