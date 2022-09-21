using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TryParseSteam
{

    public class ParserManager
    {
        bool ShowMessages = false;
        public ParserManager()
        {

        }
        DataBaseManager mngr = new DataBaseManager();

        //string json=new ParserManager().GetJsonString();
        public void SaveJsonString()
        {
            GameDSTableAdapters.GAME_LISTTableAdapter adapter = new GameDSTableAdapters.GAME_LISTTableAdapter();
            GameDS.GAME_LISTDataTable table = new GameDS.GAME_LISTDataTable();
            string res_json = "";
            adapter.GET_JSON_LIST(ref res_json);
            //adapter.Fill(table);
            string JSONString = string.Empty;
            JSONString = JValue.Parse(res_json).ToString(Formatting.Indented);
            using (StreamWriter sw = new StreamWriter("db_info.json"))
            {
                sw.WriteLine(JSONString);
            }
        }
        // SECOND
        public void StartSteamkey()
        {
            PageReader reader = new PageReader(eProxyRegion.NONE);
            reader.ReadSteamKeyPages();
            mngr.InsertSteamKeyItems(reader.OtherSiteItems);
        }
        // FIRST
        public void StartSteamAccount()
        {
            PageReader reader = new PageReader(eProxyRegion.NONE);
            reader.ReadSteamAccountPages();
            mngr.InsertSteamAccountItems(reader.OtherSiteItems);
        }

        public void Start()
        {



            PageReader readerUS = new PageReader(eProxyRegion.USA);

            readerUS.ReadAllNamesProxy();
            mngr.InsertAllDB(readerUS.ResultJsonIDs);
            Debug.WriteLineIf(ShowMessages, "INSERTION COMPLETED");

            string[] querryPrice = mngr.CreatePriceQuerry(1000);

            var taskRu = Task.Factory.StartNew(() => UpdateRUPrices(querryPrice));
            var taskKz = Task.Factory.StartNew(() => UpdateKZPrices(querryPrice));
            var taskTr = Task.Factory.StartNew(() => UpdateTRPrices(querryPrice));
            var taskUs = Task.Factory.StartNew(() => UpdateUSAPrices(querryPrice));
            Debug.WriteLineIf(ShowMessages, "THREADS STARTED");

            Task.WaitAll(taskRu, taskKz, taskTr, taskUs);

            Debug.WriteLineIf(ShowMessages, "FULL UPDATE STARTED");
            mngr.UpdatePricesRegions();


        }

        void UpdateRUPrices(string[] querryPrice)
        {
            PageReader readerRU = new PageReader(eProxyRegion.NONE);
            readerRU.ReadAllPrices(querryPrice);
            mngr.UpdateRuPrices(readerRU.ResultPrices);
            Debug.WriteLineIf(ShowMessages, "RU PRICES UPDATED");

        }

        void UpdateKZPrices(string[] querryPrice)
        {
            PageReader readerKZ = new PageReader(eProxyRegion.KZ);
            readerKZ.ReadAllPrices(querryPrice);
            mngr.UpdateKZPrices(readerKZ.ResultPrices);
            Debug.WriteLineIf(ShowMessages, "KZ PRICES UPDATED");

        }

        void UpdateUSAPrices(string[] querryPrice)
        {
            PageReader readerUS = new PageReader(eProxyRegion.USA);
            readerUS.ReadAllPrices(querryPrice);
            mngr.UpdatePrices(readerUS.ResultPrices);
            Debug.WriteLineIf(ShowMessages, "USA PRICES UPDATED");

        }

        void UpdateTRPrices(string[] querryPrice)
        {
            PageReader readerTR = new PageReader(eProxyRegion.TUR);
            readerTR.ReadAllPrices(querryPrice);
            mngr.UpdateTRPrices(readerTR.ResultPrices);
            Debug.WriteLineIf(ShowMessages, "TR PRICES UPDATED");

        }

    }
}