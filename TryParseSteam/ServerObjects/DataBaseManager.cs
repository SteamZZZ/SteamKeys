using LogicObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TryParseSteam;
using TryParseSteam.LogicObjects;

namespace ServerObjects
{
    public class DataBaseManager
    {
        object locker = new object();
        public void UpdateKzPrices(ConcurrentBag<GameItem> _items)
        {
            TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMPTableAdapter adapter = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMPTableAdapter();
            adapter.ClearBeforeFill = true;
            GameDS.GAME_LIST_TEMPDataTable table = new GameDS.GAME_LIST_TEMPDataTable();
            //int ret = adapter.Fill(table);
            GameDS.GAME_LIST_TEMPRow row = null;
            foreach (var item in _items)
            {
                double price = 0.0;

                if (Regex.IsMatch(item.Price, "[0-9]+[.[0-9]+]?"))
                {
                    var match = Regex.Match(item.Price, "[0-9]+[.[0-9]+]?").Value;
                    price = Math.Round(Convert.ToDouble(match, CultureInfo.InvariantCulture), 3);
                }


                row = table.NewGAME_LIST_TEMPRow();
                row.GLT_PRICE = price;
                row.GLT_STEAM_ID = item.App_id;
                row.GLT_STEAM_BUNDLE_ID = item.Bundle_id;
                
                table.AddGAME_LIST_TEMPRow(row);
            }
            adapter.Update(table);
            adapter.UPDATE_KZ_PRICES();
            adapter.CLEAR_TEMP_TABLE();
        }

        public void UpdateTrPrices(ConcurrentBag<GameItem> _items)
        {
            TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMPTableAdapter adapter = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMPTableAdapter();
            adapter.ClearBeforeFill = true;
            GameDS.GAME_LIST_TEMPDataTable table = new GameDS.GAME_LIST_TEMPDataTable();
            //int ret = adapter.Fill(table);
            GameDS.GAME_LIST_TEMPRow row = null;
            foreach (var item in _items)
            {
                double price = 0.0;

                if (Regex.IsMatch(item.Price, "[0-9]+[.[0-9]+]?"))
                {
                    var match = Regex.Match(item.Price, "[0-9]+[.[0-9]+]?").Value;
                    price = Math.Round(Convert.ToDouble(match, CultureInfo.InvariantCulture), 3);
                }


                row = table.NewGAME_LIST_TEMPRow();
                row.GLT_PRICE = price;
                row.GLT_STEAM_ID = item.App_id;
                row.GLT_STEAM_BUNDLE_ID = item.Bundle_id;

                table.AddGAME_LIST_TEMPRow(row);
            }
            adapter.Update(table);
            adapter.UPDATE_TR_PRICES();
            adapter.CLEAR_TEMP_TABLE();
        }

        public void UpdateRuPrices(ConcurrentBag<GameItem> _items)
        {
            TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMPTableAdapter adapter = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMPTableAdapter();
            adapter.ClearBeforeFill = true;
            GameDS.GAME_LIST_TEMPDataTable table = new GameDS.GAME_LIST_TEMPDataTable();
            //int ret = adapter.Fill(table);
            GameDS.GAME_LIST_TEMPRow row = null;
            foreach (var item in _items)
            {
                double price = 0.0;

                if (Regex.IsMatch(item.Price, "[0-9]+[.[0-9]+]?"))
                {
                    var match = Regex.Match(item.Price, "[0-9]+[.[0-9]+]?").Value;
                    price = Math.Round(Convert.ToDouble(match, CultureInfo.InvariantCulture), 3);
                }


                row = table.NewGAME_LIST_TEMPRow();
                row.GLT_PRICE = price;
                row.GLT_STEAM_ID = item.App_id;
                row.GLT_STEAM_BUNDLE_ID = item.Bundle_id;

                table.AddGAME_LIST_TEMPRow(row);
            }
            adapter.Update(table);
            adapter.UPDATE_RU_PRICES();
            adapter.CLEAR_TEMP_TABLE();
        }
        SteamEntities model = new SteamEntities();

        public void InsertAllDB(string json)
        {
            TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter adapter = new TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter();
            adapter.ClearBeforeFill = true;
            GameDS.GAME_LISTDataTable table = new GameDS.GAME_LISTDataTable();

            adapter.JSON_PARSE_ID_LIST(json);

        }

        public string[] CreatePriceQuerry(int countPerQuerry=1000)
        {
            //string resultQuerry = "https://store.steampowered.com/api/appdetails?appids=";
            TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter adapter = new TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter();
            adapter.ClearBeforeFill = true;
            GameDS.GAME_LISTDataTable table = new GameDS.GAME_LISTDataTable();
            adapter.Fill(table);

            int queeryCount = table.Count / countPerQuerry;
            int sum = countPerQuerry * queeryCount;
            int difference = table.Count - sum;

            string[] querryArray = new string[queeryCount];

            for(int i = 0; i < querryArray.Length; i++)
            {
                querryArray[i] = "https://store.steampowered.com/api/appdetails?appids=";
            }

            for (int i = 0; i < queeryCount; i++)
            {
                for(int row=i*countPerQuerry;row< (i+1) * countPerQuerry+(i==queeryCount-1?difference:0); row++)
                {
                    GameDS.GAME_LISTRow currentRow = table[row];
                    querryArray[i] += currentRow.GL_STEAM_ID + ",";
                }
                querryArray[i] += "&filters=price_overview";
            }

            //foreach(GameDS.GAME_LISTRow row in table)
            //{
            //    resultQuerry += row.GL_STEAM_ID + ",";
            //}

            return querryArray;// + "&filters=price_overview";
        }
        public void UpdateItems(ConcurrentBag<GameItem> _items)
        {
            TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMPTableAdapter adapter = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMPTableAdapter();
            adapter.ClearBeforeFill = true;
            GameDS.GAME_LIST_TEMPDataTable table = new GameDS.GAME_LIST_TEMPDataTable();
            //adapter.FillByPrice(table, 888888);
            GameDS.GAME_LIST_TEMPRow row = null;

            foreach (var item in _items)
            {

                double price = 0.0;

                if (Regex.IsMatch(item.Price, "[0-9]+[.[0-9]+]?"))
                {
                    var match = Regex.Match(item.Price, "[0-9]+[.[0-9]+]?").Value;
                    price = Math.Round(Convert.ToDouble(match, CultureInfo.InvariantCulture), 3);
                }

                //app_id = 
                //bundle_id = 

                row = table.NewGAME_LIST_TEMPRow();
                row.GLT_IMAGE_PATH = item.ImageLink;
                row.GLT_NAME = item.Name;
                row.GLT_PRICE = price;
                row.GLT_STEAM_ID = item.App_id;
                row.GLT_STEAM_BUNDLE_ID = item.Bundle_id;
                row.GLT_STEAM_REF = item.App_id == 0 ? "https://store.steampowered.com/bundle/" + item.Bundle_id : "https://store.steampowered.com/app/" + item.App_id;
                table.AddGAME_LIST_TEMPRow(row);

            }

            adapter.Update(table);
            adapter.INSERT_NEW_GAMES();
            adapter.CLEAR_TEMP_TABLE();
        }
    }
}
