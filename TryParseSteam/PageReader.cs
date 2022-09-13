using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TryParseSteam
{
    public enum eProxyRegion
    {
        NONE,
        USA,
        TUR,
        KZ
    }
    public class PageReader
    {
        public PageReader(eProxyRegion proxyType)
        {
            switch (proxyType)
            {
                case eProxyRegion.NONE:
                    proxy = null;
                    _proxyType = eProxyRegion.NONE;
                    break;
                case eProxyRegion.USA:
                    proxy = new WebProxy(new Uri("http://80.73.244.40:59244"), true, null, new NetworkCredential("ehG9tnGk", "YFUE4KS3"));
                    _proxyType = eProxyRegion.USA;

                    break;
                case eProxyRegion.KZ:
                    proxy = new WebProxy(new Uri("http://45.152.214.36:52900"), true, null, new NetworkCredential("ehG9tnGk", "YFUE4KS3"));
                    _proxyType = eProxyRegion.KZ;
                    break;
                case eProxyRegion.TUR:
                    proxy = new WebProxy(new Uri("http://45.149.131.12:48563"), true, null, new NetworkCredential("ehG9tnGk", "YFUE4KS3"));
                    _proxyType = eProxyRegion.TUR;
                    break;
            }

            ReadAllProxy();
            ParseString();
            plainText.Clear();


        }
        eProxyRegion _proxyType = eProxyRegion.NONE;
        string url = "https://store.steampowered.com/search/results/?page=1&count=100&sort_by=_ASC&ignore_preferences=1";
        ConcurrentBag<GameItem> _items = new ConcurrentBag<GameItem>();
        WebProxy proxy = new WebProxy(new Uri("http://80.73.244.40:59244"), true, null, new NetworkCredential("ehG9tnGk", "YFUE4KS3"));

        public ConcurrentBag<GameItem> Items { get => _items; set => _items = value; }

        private void ReadAllProxy()
        {
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler
                {
                    Proxy = proxy,
                    AllowAutoRedirect = false
                    ,
                    AutomaticDecompression = System.Net.DecompressionMethods.Deflate
                    | System.Net.DecompressionMethods.GZip
                    | System.Net.DecompressionMethods.None
                })
                {
                    ProcessHttpClient(hdl);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        private void ProcessHttpClient(HttpClientHandler hdl)
        {
            using (var clnt = new HttpClient(hdl) { Timeout = TimeSpan.FromMinutes(5) })
            {
                using (HttpResponseMessage resp = clnt.GetAsync(url).Result)
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        ReadHeadPage(resp);
                    }
                }
            }
        }
        

        private void ReadHeadPage(HttpResponseMessage resp)
        {
            var html = resp.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(html))
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                int pagesNumber =Convert.ToInt32( doc.DocumentNode.Descendants().Where(x => x.HasClass("search_pagination_right")).FirstOrDefault().Descendants("a").ToList()[2].InnerText);
                //dynamic res = JObject.Parse(html);
                //string parsedJson = res.results_html;
                //int itemCount = res.total_count;
                //int allPagesCount = itemCount / 100;

                Stopwatch sw = new Stopwatch();
                sw.Start();
                Parallel.For(1, pagesNumber + 1, currentPage => ReadPageProxyAsText("https://store.steampowered.com/search/results/?page="+currentPage+"&count=100&sort_by=_ASC&ignore_preferences=1", currentPage));
                sw.Stop();
                //MessageBox.Show("COUNT PARSED: " + _items.Count.ToString() + Environment.NewLine
                //    + "COUNT INITIAL: " + itemCount.ToString() + Environment.NewLine
                //    + "ELAPSED TIME: " + sw.Elapsed);

                
            }
        }
        LinkedList<string> plainText = new LinkedList<string>();
        object locker = new object();
        void ReadPageProxyAsText(string urlPage, int page)
        {
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler
                {
                    Proxy = proxy,
                    AllowAutoRedirect = false
                    ,
                    AutomaticDecompression = System.Net.DecompressionMethods.Deflate
                    | System.Net.DecompressionMethods.GZip
                    | System.Net.DecompressionMethods.None
                })
                {
                    using (var clnt = new HttpClient(hdl) { Timeout = TimeSpan.FromMinutes(1) })
                    {


                        using (HttpResponseMessage resp = clnt.GetAsync(urlPage).Result)
                        {

                            if (resp.IsSuccessStatusCode)
                            {
                                var html = resp.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(html);
                                    //dynamic res = JObject.Parse(html);
                                    //{"success":1
                                    //int succ = res.success;
                                    //var resss= 
                                    string parsedJson = doc.DocumentNode.Descendants().Where(x => x.HasClass("ignore_preferences")).FirstOrDefault().Descendants("div").ToList()[2].InnerHtml; ;
                                    lock (locker)
                                    {
                                        plainText.AddLast(parsedJson);
                                    }

                                    //doc.LoadHtml(parsedJson);
                                    //IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants("a");

                                    //AddItemsToList(nodes);
                                }
                                else
                                {
                                    Debug.WriteLine("#### EMPTY HTML");
                                }
                            }
                            else
                            {
                                Debug.WriteLine("#### ERROR ACCESS");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }
        void ParseString()
        {

            //plainText.Clear();
            
            Parallel.ForEach(plainText, textBlock => 
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(textBlock);
                IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants("a");
                foreach(var node in nodes)
                {
                    var title = node.Descendants().Where(x => x.HasClass("title")).FirstOrDefault();
                    var pri = node.Descendants().Where(x => x.HasClass("search_price")).FirstOrDefault();
                    //var disc = n.Descendants().Where(x => x.HasClass("search_discount")).FirstOrDefault();
                    var img = node.Descendants("img").FirstOrDefault();
                    string app_id = node.GetAttributeValue("data-ds-appid", "no-id");
                    string bundle_id = node.GetAttributeValue("data-ds-bundleid", "no-id");
                    string name = "";
                    string price = "";
                    string discount = "";
                    string image = "";

                    if (title != null)
                        name = title.InnerHtml;
                    if (pri != null)
                        price = pri.InnerHtml.Contains("strike") ? pri.InnerHtml.Split('>').Last() : pri.InnerHtml;
                    if (img != null)
                        image = img.GetAttributeValue("src", "no-image");



                    _items.Add(new GameItem(name, price, discount, image, app_id, bundle_id));
                }

            });

        }

        void ParseStringSimple()
        {

            //plainText.Clear();

            Parallel.ForEach(plainText, textBlock =>
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(textBlock);
                IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants("a");
                foreach (var node in nodes)
                {
                    //var title = node.Descendants().Where(x => x.HasClass("title")).FirstOrDefault();
                    var pri = node.Descendants().Where(x => x.HasClass("search_price")).FirstOrDefault();
                    //var disc = n.Descendants().Where(x => x.HasClass("search_discount")).FirstOrDefault();
                    //var img = node.Descendants("img").FirstOrDefault();
                    string app_id = node.GetAttributeValue("data-ds-appid", "no-id");
                    string bundle_id = node.GetAttributeValue("data-ds-bundleid", "no-id");
                    //string name = "";
                    string price = "";
                    //string discount = "";
                    //string image = "";

                    //if (title != null)
                    //    name = title.InnerHtml;
                    if (pri != null)
                        price = pri.InnerHtml.Contains("strike") ? pri.InnerHtml.Split('>').Last() : pri.InnerHtml;
                    //if (img != null)
                    //    image = img.GetAttributeValue("src", "no-image");



                    _items.Add(new GameItem("", price, "", "", app_id, bundle_id));
                }

            });

        }
        void ReadPageProxy(string url, int page)
        {
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler
                {
                    Proxy = proxy,
                    AllowAutoRedirect = false
                    ,
                    AutomaticDecompression = System.Net.DecompressionMethods.Deflate
                    | System.Net.DecompressionMethods.GZip
                    | System.Net.DecompressionMethods.None
                })
                {
                    using (var clnt = new HttpClient(hdl) { Timeout = TimeSpan.FromMinutes(1)})
                    {


                        using (HttpResponseMessage resp = clnt.GetAsync(url).Result)
                        {

                            if (resp.IsSuccessStatusCode)
                            {
                                var html = resp.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    dynamic res = JObject.Parse(html);
                                    string parsedJson = res.results_html;

                                    doc.LoadHtml(parsedJson);
                                    IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants("a");

                                    AddItemsToList(nodes);
                                }
                                else
                                {
                                    Debug.WriteLine("#### EMPTY HTML");
                                }
                            }
                            else
                            {
                                Debug.WriteLine("#### ERROR ACCESS");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }

        private void AddItemsToList(IEnumerable<HtmlNode> nodes)
        {
            foreach (var n in nodes)
            {
                var title = n.Descendants().Where(x => x.HasClass("title")).FirstOrDefault();
                var pri = n.Descendants().Where(x => x.HasClass("search_price")).FirstOrDefault();
                //var disc = n.Descendants().Where(x => x.HasClass("search_discount")).FirstOrDefault();
                var img = n.Descendants("img").FirstOrDefault();
                string app_id = n.GetAttributeValue("data-ds-appid", "no-id");
                string bundle_id = n.GetAttributeValue("data-ds-bundleid", "no-id");
                string name = "";
                string price = "";
                string discount = "";
                string image = "";

                if (title != null)
                    name = title.InnerHtml;
                if (pri != null)
                    price = pri.InnerHtml.Contains("strike") ? pri.InnerHtml.Split('>').Last() : pri.InnerHtml;
                if (img != null)
                    image = img.GetAttributeValue("src", "no-image");



                _items.Add(new GameItem(name, price, discount, image, app_id, bundle_id));
            }
        }

    }
}
