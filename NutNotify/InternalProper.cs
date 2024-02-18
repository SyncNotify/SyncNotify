using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SyncNotify
{
    internal class InternalProper
    {
        private static string recentFileName;
        private static string recentText;
        private static string recentTitle;
        private static string recentDescription;
        private static string recentDate;
        private static string recentTime;

        public static string getVersion()
        {
            return Application.ResourceAssembly.GetName().Version.ToString();
        }

        public static string RecentFileName { get => recentFileName; set => recentFileName = value; }
        public static string RecentText { get => recentText; set => recentText = value; }
    }
}
