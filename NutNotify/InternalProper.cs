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
        private static string recentText;


        public static string getVersion()
        {
            return Application.ResourceAssembly.GetName().Version.ToString();
        }

        public static string RecentText { get => recentText; set => recentText = value; }
    }
}
