using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TryParseSteam
{
    public partial class Form1 : Form
    {
        DataBaseManager mngr = new DataBaseManager();
        public Form1()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            PageReader reader = new PageReader(eProxyRegion.USA);
            //mngr.UpdateItems(reader.Items);
            mngr.InsertAllDB(reader.Items);
            reader = new PageReader(eProxyRegion.NONE);
            mngr.UpdateRuPrices(reader.Items);

            reader = new PageReader(eProxyRegion.KZ);
            mngr.UpdateKzPrices(reader.Items);

            reader = new PageReader(eProxyRegion.TUR);
            mngr.UpdateTrPrices(reader.Items);

            sw.Stop();
            Debug.WriteLine(sw.Elapsed, "FULL UPDATE ");
        }
    }



}
