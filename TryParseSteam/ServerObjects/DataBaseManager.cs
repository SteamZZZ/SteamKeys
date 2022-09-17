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
using System.Threading;
using System.Threading.Tasks;
using TryParseSteam;
using TryParseSteam.LogicObjects;

namespace ServerObjects
{
    public class DataBaseManager
    {
        object locker = new object();

        public void InsertAllDB(string json)
        {
            TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter adapter = new TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter();
            adapter.ClearBeforeFill = true;
            adapter.Timeout = 180;
            GameDS.GAME_LISTDataTable table = new GameDS.GAME_LISTDataTable();
            try
            {
                adapter.JSON_PARSE_ID_LIST(json);
            }
            catch(Exception ex)
            {

            }
            

        }

        public string[] CreatePriceQuerry(int countPerQuerry=1000)
        {
            //string resultQuerry = "https://store.steampowered.com/api/appdetails?method=post&appids=";
            TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter adapter = new TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter();
            adapter.ClearBeforeFill = true;
            adapter.Timeout = 180;
            GameDS.GAME_LISTDataTable table = new GameDS.GAME_LISTDataTable();
            adapter.Fill(table);

            int queeryCount = table.Count / countPerQuerry;
            int sum = countPerQuerry * queeryCount;
            int difference = table.Count - sum;

            string[] querryArray; 


            if (difference > 0)
            {
                querryArray= new string[queeryCount+1];
                for (int i = 0; i < querryArray.Length; i++)
                {
                    querryArray[i] = "https://store.steampowered.com/api/appdetails?method=post&appids=";
                }
                for (int i = 0; i < queeryCount + 1; i++)
                {
                    for (int row = i * countPerQuerry; row < (i == queeryCount? difference+ i * countPerQuerry : (i + 1) * countPerQuerry); row++)
                    {
                        GameDS.GAME_LISTRow currentRow = table[row];
                        querryArray[i] += currentRow.GL_STEAM_ID + ",";
                    }
                    querryArray[i] += "&filters=price_overview";
                }
            }
            else
            {
                querryArray = new string[queeryCount ];
                for (int i = 0; i < querryArray.Length; i++)
                {
                    querryArray[i] = "https://store.steampowered.com/api/appdetails?method=post&appids=";
                }
                for (int i = 0; i < queeryCount ; i++)
                {
                    for (int row = i * countPerQuerry; row < (i + 1) * countPerQuerry; row++)
                    {
                        GameDS.GAME_LISTRow currentRow = table[row];
                        querryArray[i] += currentRow.GL_STEAM_ID + ",";
                    }
                    querryArray[i] += "&filters=price_overview";
                }
            }


            //foreach(GameDS.GAME_LISTRow row in table)
            //{
            //    resultQuerry += row.GL_STEAM_ID + ",";
            //}

            return querryArray;// + "&filters=price_overview";
        }
        Semaphore lockUs = new Semaphore(15, 15);
        public void UpdatePrices(string[] querryResults)
        {
            //TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter adapter = new TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter();
            //adapter.ClearBeforeFill = true;
            //adapter.Timeout = 180;

            //for (int i = 0; i < querryResults.Length; i++)
            //{
            //    adapter.UPDATE_USA_PRICES_JSON(querryResults[i]);
            //}

            Parallel.For(0, querryResults.Length, i => 
            {
                TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_USATableAdapter adapter 
                = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_USATableAdapter();
                adapter.ClearBeforeFill = true;
                
                adapter.Timeout = 120;
                adapter.UPDATE_USA_PRICES_JSON(querryResults[i]);
                //adapter.Dispose();
            });

        }
        //object locker = new object();
        Semaphore lockRu = new Semaphore(15, 15);
        public void UpdateRuPrices(string[] querryResults)
        {
            //TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_RUTableAdapter adapter 
            //    = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_RUTableAdapter();
            //adapter.ClearBeforeFill = true;
            ////adapter.Timeout = 180;
            //for(int i = 0; i < querryResults.Length; i++)
            //{
            //    adapter.UPDATE_RU_PRICES_JSON(querryResults[i]);
            //}
            Parallel.For(0, querryResults.Length, i =>
            {
                TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_RUTableAdapter adapter 
                = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_RUTableAdapter();
                adapter.ClearBeforeFill = true;
                adapter.Timeout = 120;
                adapter.UPDATE_RU_PRICES_JSON(querryResults[i]);
                adapter.Dispose();

            });


        }


        public void UpdateKZPrices(string[] querryResults)
        {
            //TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_KZTableAdapter adapter 
            //    = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_KZTableAdapter();
            //adapter.ClearBeforeFill = true;
            ////adapter.Timeout = 180;
            //for (int i = 0; i < querryResults.Length; i++)
            //{
            //    adapter.UPDATE_KZ_PRICES_JSON(querryResults[i]);
            //}

            Parallel.For(0, querryResults.Length, i => 
            {
                TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_KZTableAdapter adapter
    = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_KZTableAdapter();
                adapter.ClearBeforeFill = true;
                adapter.Timeout = 120;
                adapter.UPDATE_KZ_PRICES_JSON(querryResults[i]);
                adapter.Dispose();
            });
        }

        public void UpdateTRPrices(string[] querryResults)
        {
            //TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_TRTableAdapter adapter 
            //    = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_TRTableAdapter();
            //adapter.ClearBeforeFill = true;
            ////adapter.Timeout = 180;
            //for (int i = 0; i < querryResults.Length; i++)
            //{
            //    adapter.UPDATE_TR_PRICES_JSON(querryResults[i]);
            //}
            Parallel.For(0, querryResults.Length, i => 
            {
                TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_TRTableAdapter adapter
    = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_TRTableAdapter();
                adapter.ClearBeforeFill = true;
                adapter.Timeout = 120;
                adapter.UPDATE_TR_PRICES_JSON(querryResults[i]);
                adapter.Dispose();
            });
        }

        public void UpdatePricesRegions()
        {
            TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter adapter
                = new TryParseSteam.GameDSTableAdapters.GAME_LISTTableAdapter();
            adapter.ClearBeforeFill = true;
            adapter.Timeout = 180;
            adapter.UPDATE_REGION_PRICES();
        }
    }
}
