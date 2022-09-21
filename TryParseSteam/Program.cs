using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TryParseSteam
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            ParserManager parser = new ParserManager();
            parser.Start();
            parser.StartSteamAccount();
            parser.StartSteamkey();
            //parser.SaveJsonString();
            sw.Stop();
            Debug.WriteLine(sw.Elapsed, "FULL UPDATE ");
        }
    }
}