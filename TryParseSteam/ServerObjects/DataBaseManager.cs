using LogicObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
            catch(Exception ex) { }

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

            return querryArray;// + "&filters=price_overview";
        }

        public void AddUser(string login,string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                TryParseSteam.UserDSTableAdapters.USER_LISTTableAdapter adapter = new TryParseSteam.UserDSTableAdapters.USER_LISTTableAdapter();
                UserDS.USER_LISTDataTable table = new UserDS.USER_LISTDataTable();
                adapter.FillByLogin(table, login);
                if (table.Count == 0)
                    adapter.ADD_USER(login, password);
                else
                    throw new Exception("User with this login already exists");
            }
            else
                throw new Exception("Empty credentials");

        }

        public User VerifyUser(string login,string password)
        {
            TryParseSteam.UserDSTableAdapters.USER_LISTTableAdapter adapter = new TryParseSteam.UserDSTableAdapters.USER_LISTTableAdapter();
            adapter.ClearBeforeFill = true;
            UserDS.USER_LISTDataTable table = new UserDS.USER_LISTDataTable();
            adapter.FillByCredentials(table, login, password);
            if (table.Count == 1)
            {
                var row = table[0];
                return new User(row.UL_ID, row.UL_LOGIN);
            }
            return null;
        }

        public List<int> GetUserFavorites(int user_id)
        {
            List<int> game_ids = new List<int>();
            TryParseSteam.UserDSTableAdapters.USER_FAVORITESTableAdapter adapter = new TryParseSteam.UserDSTableAdapters.USER_FAVORITESTableAdapter();
            UserDS.USER_FAVORITESDataTable table = new UserDS.USER_FAVORITESDataTable();
            adapter.ClearBeforeFill = true;
            adapter.FillByUserId(table, user_id);
            foreach(UserDS.USER_FAVORITESRow row in table)
            {
                game_ids.Add(row.UF_GL_STEAM_ID);
            }
            return game_ids;
        }

        public void UpdatePrices(string[] querryResults)
        {

            Parallel.For(0, querryResults.Length, i => 
            {
                TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_USATableAdapter adapter 
                = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_USATableAdapter();
                adapter.ClearBeforeFill = true;
                
                adapter.Timeout = 180;
                adapter.UPDATE_USA_PRICES_JSON(querryResults[i]);
                //adapter.Dispose();
            });

        }
        // FIRST
        public void InsertSteamAccountItems(ConcurrentBag<OtherSiteItem> _items)
        {
            TryParseSteam.GameDSTableAdapters.OTHER_SITE_LISTTableAdapter adapter
= new TryParseSteam.GameDSTableAdapters.OTHER_SITE_LISTTableAdapter();
            adapter.Timeout = 180;
            GameDS.OTHER_SITE_LISTDataTable table = new GameDS.OTHER_SITE_LISTDataTable();
            adapter.CLEAR_OTHER_SITE_LIST();
            GameDS.OTHER_SITE_LISTRow row = null;
            //Parallel.ForEach(_items, item => 
            //{
            //    row = table.NewSTEAM_ACCOUNT_LISTRow();
            //    row.SAL_NAME = item.Name.Contains("купить") ? item.Name.Remove(item.Name.IndexOf(" купить")) : item.Name;
            //    row.SAL_PRICE = item.Price;
            //    row.SAL_REF = item.Href.Contains("http") ? item.Href : "https://steam-account.ru" + item.Href;
            //    table.AddSTEAM_ACCOUNT_LISTRow(row);
            //});
            foreach (var item in _items)
            {
                row = table.NewOTHER_SITE_LISTRow();
                row.OSL_SITE_NAME = "SteamAccount";
                row.OSL_NAME = item.Name.Contains("купить") ? item.Name.Remove(item.Name.IndexOf(" купить")) : item.Name;
                row.OSL_PRICE = item.Price;
                row.OSL_REF = item.Href.Contains("http") ? item.Href : "https://steam-account.ru" + item.Href;
                table.AddOTHER_SITE_LISTRow(row);
            }
            adapter.Update(table);
            adapter.UPDATE_IDS_OTHER_SITE_LIST();

        }
        // SECOND
        public void InsertSteamKeyItems(ConcurrentBag<OtherSiteItem> _items)
        {
            TryParseSteam.GameDSTableAdapters.OTHER_SITE_LISTTableAdapter adapter
= new TryParseSteam.GameDSTableAdapters.OTHER_SITE_LISTTableAdapter();
            adapter.Timeout = 180;
            GameDS.OTHER_SITE_LISTDataTable table = new GameDS.OTHER_SITE_LISTDataTable();
            //adapter.c();
            GameDS.OTHER_SITE_LISTRow row = null;

            foreach (var item in _items)
            {
                row = table.NewOTHER_SITE_LISTRow();
                row.OSL_SITE_NAME = "SteamKey";
                row.OSL_NAME =  item.Name;
                row.OSL_PRICE = item.Price;
                row.OSL_REF = "https://steamkey.com" + item.Href;
                table.AddOTHER_SITE_LISTRow(row);
            }
            adapter.Update(table);
            adapter.UPDATE_IDS_OTHER_SITE_LIST();

        }

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
                adapter.Timeout = 180;
                adapter.UPDATE_RU_PRICES_JSON(querryResults[i]);
                adapter.Dispose();

            });


        }

        public void UpdateKZPrices(string[] querryResults)
        {
            Parallel.For(0, querryResults.Length, i => 
            {
                TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_KZTableAdapter adapter
    = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_KZTableAdapter();
                adapter.ClearBeforeFill = true;
                adapter.Timeout = 180;
                adapter.UPDATE_KZ_PRICES_JSON(querryResults[i]);
                adapter.Dispose();
            });
        }

        public void UpdateTRPrices(string[] querryResults)
        {
            Parallel.For(0, querryResults.Length, i => 
            {
                TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_TRTableAdapter adapter
    = new TryParseSteam.GameDSTableAdapters.GAME_LIST_TEMP_TRTableAdapter();
                adapter.ClearBeforeFill = true;
                adapter.Timeout = 180;
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
