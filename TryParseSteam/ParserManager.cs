using ServerObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryParseSteam
{
    
    public class ParserManager
    {
        public ParserManager()
        {
            Start();
        }
        DataBaseManager mngr = new DataBaseManager();

        private void Start()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            PageReader reader = new PageReader(eProxyRegion.USA);
            reader.ReadAllNamesProxy();
            //string[] querryPrice = mngr.CreatePriceQuerry(700);
            //reader.ReadAllPrices(querryPrice);
            //var items = reader.ResultPrices;
            //mngr.UpdateItems(reader.Items);
            mngr.InsertAllDB(reader.ResultJsonIDs);
            //reader = new PageReader(eProxyRegion.NONE);
            //mngr.UpdateRuPrices(reader.Items);

            //reader = new PageReader(eProxyRegion.KZ);
            //mngr.UpdateKzPrices(reader.Items);

            //reader = new PageReader(eProxyRegion.TUR);
            //mngr.UpdateTrPrices(reader.Items);

            sw.Stop();
            Debug.WriteLine(sw.Elapsed, "FULL UPDATE ");
        }
    }
}
