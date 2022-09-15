using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicObjects
{
    public class GameItem
    {
        string _name = "";
        string _price = "";
        //string _discount = "";
        string _imageLink = "";
        int _app_id = 0;
        int _bundle_id = 0;

        public string Name { get => _name; set => _name = value; }
        public string Price { get => _price; set => _price = value; }
        //public string Discount { get => _discount; set => _discount = value; }
        public string ImageLink { get => _imageLink; set => _imageLink = value; }
        public int App_id { get => _app_id; set => _app_id = value; }
        public int Bundle_id { get => _bundle_id; set => _bundle_id = value; }

        public GameItem() { }
        public GameItem(string name,string price,string discount,string image,string appId,string bundle_id)
        {
            _name = name;
            _price = price;
            //_discount = discount;
            _imageLink = image;
            _app_id = appId == "no-id" ||string.IsNullOrEmpty(appId) ? 0 : appId.Contains(",") ? Convert.ToInt32(appId.Split(',')[0]) : Convert.ToInt32(appId);
            _bundle_id = bundle_id == "no-id" || string.IsNullOrEmpty(appId) ? 0 : bundle_id.Contains(",") ? Convert.ToInt32(bundle_id.Split(',')[0]) : Convert.ToInt32(bundle_id);
        }
    }
}
