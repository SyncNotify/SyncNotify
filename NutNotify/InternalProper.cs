using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutNotify
{
    internal class InternalProper
    {
        private static string recentFileName;
        private static string recentText;

        public static string RecentFileName { get => recentFileName; set => recentFileName = value; }
        public static string RecentText { get => recentText; set => recentText = value; }
    }
}
